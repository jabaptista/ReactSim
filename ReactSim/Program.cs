using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel para escutar apenas HTTP na porta 8080
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // apenas HTTP
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();

// Register repository and services
builder.Services.AddSingleton<ReactSim.Repositories.IDataContext, ReactSim.Repositories.MongoDBContext>();
builder.Services.AddSingleton<ReactSim.Repositories.IMongoDbRepository, ReactSim.Repositories.MongoDBRepository>();
builder.Services.AddSingleton<ReactSim.Repositories.QuestionRepository>();
builder.Services.AddSingleton<ReactSim.Repositories.IQuestionRepository>(sp =>
    new ReactSim.Repositories.QuestionRepositoryProxy(
        sp.GetRequiredService<ReactSim.Repositories.QuestionRepository>(),
        sp.GetRequiredService<ILogger<ReactSim.Repositories.QuestionRepositoryProxy>>(),
        sp.GetRequiredService<IMemoryCache>()));
builder.Services.AddSingleton<ReactSim.Adapters.IQuestionDtoAdapter, ReactSim.Adapters.QuestionDtoAdapter>();
builder.Services.AddSingleton<ReactSim.Adapters.IQuestionDboAdapter, ReactSim.Adapters.QuestionDboAdapter>();
builder.Services.AddSingleton<ReactSim.Services.IQuestionService, ReactSim.Services.QuestionService>();

var app = builder.Build();

// Optional explicit API base from config/env (can override dynamic detection)
var apiBaseFromConfig = builder.Configuration["CLIENT_API_URL"]
    ?? builder.Configuration["API_BASE_URL"]
    ?? builder.Configuration["REACT_APP_API_URL"];

// Expose /env.js; determine base URL dynamically from the incoming request so it reflects
// the exposed host:port (useful when running in Docker with port mappings).
app.MapGet("/env.js", (HttpContext ctx) =>
{
    var req = ctx.Request;

    // honor proxy headers if present (X-Forwarded-Proto / X-Forwarded-Host)
    req.Headers.TryGetValue("X-Forwarded-Proto", out var forwardedProtoValues);
    req.Headers.TryGetValue("X-Forwarded-Host", out var forwardedHostValues);

    var scheme = !string.IsNullOrEmpty(forwardedProtoValues.ToString()) ? forwardedProtoValues.ToString() : req.Scheme;
    var host = !string.IsNullOrEmpty(forwardedHostValues.ToString()) ? forwardedHostValues.ToString() : (req.Host.HasValue ? req.Host.Value : null);

    string baseUrl;
    if (!string.IsNullOrEmpty(apiBaseFromConfig))
    {
        baseUrl = apiBaseFromConfig.TrimEnd('/');
    }
    else if (!string.IsNullOrEmpty(host))
    {
        baseUrl = $"{scheme}://{host}";
    }
    else
    {
        // fallback to container default
        baseUrl = $"http://localhost:8080";
    }

    var payload = new { API_URL = baseUrl };
    var json = System.Text.Json.JsonSerializer.Serialize(payload);
    var js = $"window.__env__ = {json};";
    return Results.Content(js, "application/javascript");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
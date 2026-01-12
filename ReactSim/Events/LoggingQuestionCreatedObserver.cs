using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ReactSim.Events
{
    public class LoggingQuestionCreatedObserver : IQuestionCreatedObserver
    {
        private readonly ILogger<LoggingQuestionCreatedObserver> logger;

        public LoggingQuestionCreatedObserver(ILogger<LoggingQuestionCreatedObserver> logger)
        {
            this.logger = logger;
        }

        public Task OnQuestionCreatedAsync(QuestionCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Question criada (Id={Id}, Descrição={Description}) às {Timestamp}",
                @event.Question?.Id,
                @event.Question?.Description,
                @event.OccurredAtUtc);
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ReactSim.Domain.Model;
using ReactSim.DTO.Questions;
using ReactSim.Services;
using System.Threading.Tasks;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FormCreationRequest formCreationRequest)
        {
            foreach (var question in formCreationRequest.Questions)
            {
                var builder = new QuestionBuilder()
                    .WithId(question.Id)
                    .WithDescription(question.Description)
                    .WithCompetencies(question.Competencies)
                    .WithRightAwnser(question.RightAwnser)
                    .WithOptions(question.Options.Select(q => new Domain.Model.AwnserOption(q.Id, q.Text)));

                question.MediaURLs?.ToList().ForEach(url => builder.AddMediaUrl(url));

                await questionService.CreateQuestionsAsync(builder.Build());

            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await questionService.GetAllQuestionsAsync();

            return questions.Select(x => new DTO.Questions.Question
            {
                Id = x.Id,
                Description = x.Description,
                Competencies = x.Competencies,
                RightAwnser = x.RightAwnser,
                MediaURLs = x.MediaURL,
                Options = x.Options.Select(op => new DTO.Questions.AwnserOption() { Id = op.Id, Text = op.Text })
            }) is var result
                ? Ok(result)
                : NotFound();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ReactSim.Adapters;
using ReactSim.DTO.Questions;
using ReactSim.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionService questionService;
        private readonly IQuestionDtoAdapter questionAdapter;

        public QuestionController(IQuestionService questionService, IQuestionDtoAdapter questionAdapter)
        {
            this.questionService = questionService;
            this.questionAdapter = questionAdapter;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FormCreationRequest formCreationRequest)
        {
            foreach (var question in formCreationRequest.Questions ?? Enumerable.Empty<Question>())
            {
                var domainQuestion = questionAdapter.FromDto(question);
                await questionService.CreateQuestionsAsync(domainQuestion);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await questionService.GetAllQuestionsAsync();
            var dtoQuestions = questions?.Select(questionAdapter.ToDto).ToList() ?? new List<Question>();

            return dtoQuestions.Any()
                ? Ok(dtoQuestions)
                : NotFound();
        }
    }
}
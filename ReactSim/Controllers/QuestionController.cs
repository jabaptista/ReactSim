using Microsoft.AspNetCore.Mvc;
using ReactSim.Adapters;
using ReactSim.DTO.Questions;
using ReactSim.Services;
using ReactSim.Validation;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionService questionService;
        private readonly IQuestionDtoAdapter questionAdapter;
        private readonly IFormCreationRequestValidationPipeline validationPipeline;

        public QuestionController(IQuestionService questionService, IQuestionDtoAdapter questionAdapter, IFormCreationRequestValidationPipeline validationPipeline)
        {
            this.questionService = questionService;
            this.questionAdapter = questionAdapter;
            this.validationPipeline = validationPipeline;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FormCreationRequest formCreationRequest)
        {
            if (formCreationRequest == null)
            {
                return BadRequest("Pedido inválido: faltam dados do formulário.");
            }

            var validationErrors = validationPipeline.Validate(formCreationRequest);
            if (validationErrors.Count > 0)
            {
                return BadRequest(new { Errors = validationErrors });
            }

            foreach (var question in formCreationRequest.Questions ?? Enumerable.Empty<Question>())
            {
                question.ActivityId = formCreationRequest.ActivityId;
                var domainQuestion = questionAdapter.FromDto(question);
                await questionService.CreateQuestionsAsync(domainQuestion);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] string activityId)
        {
            if (string.IsNullOrWhiteSpace(activityId))
            {
                return BadRequest("activityId é obrigatório.");
            }

            var questions = await questionService.GetQuestionsByActivityAsync(activityId);
            var dtoQuestions = questions?.Select(questionAdapter.ToDto).ToList() ?? new List<Question>();

            return dtoQuestions.Any()
                ? Ok(dtoQuestions)
                : NotFound();
        }
    }
}
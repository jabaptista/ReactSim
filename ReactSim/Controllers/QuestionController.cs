using Microsoft.AspNetCore.Mvc;
using ReactSim.Adapters;
using ReactSim.DTO.Questions;
using ReactSim.Services;
using ReactSim.Validation;

namespace ReactSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        private readonly IQuestionDtoAdapter questionAdapter;
        private readonly IFormCreationRequestValidationPipeline validationPipeline;
        private readonly IActivityService activityService;

        public QuestionController(IQuestionService questionService, IQuestionDtoAdapter questionAdapter, IFormCreationRequestValidationPipeline validationPipeline, IActivityService activityService)
        {
            this.questionService = questionService;
            this.questionAdapter = questionAdapter;
            this.validationPipeline = validationPipeline;
            this.activityService = activityService;
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

            await activityService.EnsureDraftAsync(formCreationRequest.ActivityId).ConfigureAwait(false);

            foreach (var question in formCreationRequest.Questions ?? Enumerable.Empty<Question>())
            {
                var domainQuestion = questionAdapter.FromDto(question, formCreationRequest.ActivityId);
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
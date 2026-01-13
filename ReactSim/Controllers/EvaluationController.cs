using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactSim.Adapters;
using ReactSim.DTO.Evaluation;
using ReactSim.Services;

namespace ReactSim.Controllers
{
    [Route("api/evaluations")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly ILogger<EvaluationController> logger;
        private readonly IEvaluationService evaluationService;
        private readonly IEvaluationDtoAdapter evaluationDtoAdapter;

        public EvaluationController(
            ILogger<EvaluationController> logger,
            IEvaluationService evaluationService,
            IEvaluationDtoAdapter evaluationDtoAdapter)
        {
            this.logger = logger;
            this.evaluationService = evaluationService;
            this.evaluationDtoAdapter = evaluationDtoAdapter;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] SubmitEvaluationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Pedido inválido: faltam dados da avaliação.");
            }

            if (string.IsNullOrWhiteSpace(request.CandidateId) || string.IsNullOrWhiteSpace(request.EvaluationId))
            {
                return BadRequest("Pedido inválido: identifique o candidato e a avaliação.");
            }

            if (request.Answers == null || request.Answers.Count == 0)
            {
                return BadRequest("Pedido inválido: nenhuma resposta foi enviada.");
            }

            var submission = evaluationDtoAdapter.FromRequest(request);
            if (submission.Answers.Count == 0)
            {
                return BadRequest("Pedido inválido: nenhuma resposta válida foi enviada.");
            }

            var result = await evaluationService.EvaluateAsync(submission).ConfigureAwait(false);

            logger.LogInformation(
                "Avaliação recebida. CandidateId={CandidateId}, EvaluationId={EvaluationId}, Answers={AnswerCount}, CompletionRate={CompletionRate}, Correct={CorrectAnswers}",
                result.CandidateId,
                result.EvaluationId,
                result.AnswersReceived,
                result.CompletionRate,
                result.CorrectAnswers);

            var response = evaluationDtoAdapter.ToResponse(result);
            return Ok(response);
        }
    }
}

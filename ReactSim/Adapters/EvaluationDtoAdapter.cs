using System;
using System.Linq;
using ReactSim.DTO.Evaluation;
using DomainSubmission = ReactSim.Domain.Model.EvaluationSubmission;
using DomainResult = ReactSim.Domain.Model.EvaluationResult;
using DomainAnswer = ReactSim.Domain.Model.SubmittedAnswer;
using DomainCompetencyScore = ReactSim.Domain.Model.CompetencyScore;

namespace ReactSim.Adapters
{
    public class EvaluationDtoAdapter : IEvaluationDtoAdapter
    {
        public DomainSubmission FromRequest(SubmitEvaluationRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var answers = (request.Answers ?? Enumerable.Empty<SubmittedAnswer>())
                .Select(a => new
                {
                    Parsed = int.TryParse(a.QuestionId, out var qId) && int.TryParse(a.SelectedOptionId, out var optId),
                    QuestionId = int.TryParse(a.QuestionId, out var q) ? q : 0,
                    OptionId = int.TryParse(a.SelectedOptionId, out var o) ? o : 0
                })
                .Where(x => x.Parsed)
                .Select(x => new DomainAnswer(x.QuestionId, x.OptionId))
                .ToList();

            return new DomainSubmission(
                request.CandidateId ?? string.Empty,
                request.EvaluationId ?? string.Empty,
                request.Timestamp,
                request.CompletionRate,
                answers);
        }

        public SubmitEvaluationResponse ToResponse(DomainResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            return new SubmitEvaluationResponse
            {
                Message = "Avaliação recebida com sucesso.",
                CandidateId = result.CandidateId,
                EvaluationId = result.EvaluationId,
                AnswersReceived = result.AnswersReceived,
                EvaluatedAnswers = result.EvaluatedAnswers,
                CorrectAnswers = result.CorrectAnswers,
                CompletionRate = result.CompletionRate,
                Timestamp = result.Timestamp,
                CompetencyScores = result.CompetencyScores
                    .Select(c => new CompetencyScoreResponse
                    {
                        CompetencyId = c.CompetencyId,
                        Correct = c.Correct
                    })
                    .OrderBy(c => c.CompetencyId)
                    .ToList()
            };
        }
    }
}

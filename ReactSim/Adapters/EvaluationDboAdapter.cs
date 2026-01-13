using System;
using System.Collections.Generic;
using System.Linq;
using ReactSim.Domain.Model;
using DboEvaluationResult = ReactSim.Repositories.dbo.EvaluationResult;
using DboCompetencyScore = ReactSim.Repositories.dbo.CompetencyScore;

namespace ReactSim.Adapters
{
    public class EvaluationDboAdapter : IEvaluationDboAdapter
    {
        public DboEvaluationResult ToDbo(EvaluationResult domain)
        {
            ArgumentNullException.ThrowIfNull(domain);

            return new DboEvaluationResult
            {
                CandidateId = domain.CandidateId,
                EvaluationId = domain.EvaluationId,
                AnswersReceived = domain.AnswersReceived,
                EvaluatedAnswers = domain.EvaluatedAnswers,
                CorrectAnswers = domain.CorrectAnswers,
                CompletionRate = domain.CompletionRate,
                Timestamp = domain.Timestamp,
                CompetencyScores = (domain.CompetencyScores ?? new List<CompetencyScore>())
                    .Select(c => new DboCompetencyScore
                    {
                        CompetencyId = c.CompetencyId,
                        Correct = c.Correct
                    })
                    .ToList()
            };
        }
    }
}

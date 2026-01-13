using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactSim.Domain.Model;
using ReactSim.Repositories;

namespace ReactSim.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IEvaluationRepository evaluationRepository;

        public EvaluationService(IQuestionRepository questionRepository, IEvaluationRepository evaluationRepository)
        {
            this.questionRepository = questionRepository;
            this.evaluationRepository = evaluationRepository;
        }

        public async Task<EvaluationResult> EvaluateAsync(EvaluationSubmission submission)
        {
            ArgumentNullException.ThrowIfNull(submission);

            var questions = (await questionRepository.GetAllAsync().ConfigureAwait(false))
                ?.ToDictionary(q => q.Id) ?? new Dictionary<int, Question>();

            var competencyHits = new Dictionary<int, int>();
            var totalCorrect = 0;
            var evaluated = 0;

            foreach (var answer in submission.Answers)
            {
                if (!questions.TryGetValue(answer.QuestionId, out var question))
                {
                    continue;
                }

                evaluated++;

                if (answer.SelectedOptionId == question.RightAwnser)
                {
                    totalCorrect++;

                    foreach (var comp in question.Competencies ?? Enumerable.Empty<int>())
                    {
                        if (!competencyHits.ContainsKey(comp))
                        {
                            competencyHits[comp] = 0;
                        }

                        competencyHits[comp]++;
                    }
                }
            }

            var result = new EvaluationResult(
                submission.CandidateId,
                submission.EvaluationId,
                submission.Answers.Count,
                evaluated,
                totalCorrect,
                competencyHits.Select(kvp => new CompetencyScore(kvp.Key, kvp.Value)),
                submission.CompletionRate,
                submission.Timestamp);

            await evaluationRepository.SaveAsync(result).ConfigureAwait(false);

            return result;
        }
    }
}

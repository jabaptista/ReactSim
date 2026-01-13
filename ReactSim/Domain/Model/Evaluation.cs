using System.Collections.Generic;
using System.Linq;

namespace ReactSim.Domain.Model
{
    public class EvaluationSubmission
    {
        public string CandidateId { get; }
        public string EvaluationId { get; }
        public string? Timestamp { get; }
        public string? CompletionRate { get; }
        public IReadOnlyCollection<SubmittedAnswer> Answers { get; }

        public EvaluationSubmission(string candidateId, string evaluationId, string? timestamp, string? completionRate, IEnumerable<SubmittedAnswer> answers)
        {
            CandidateId = candidateId ?? string.Empty;
            EvaluationId = evaluationId ?? string.Empty;
            Timestamp = timestamp;
            CompletionRate = completionRate;
            Answers = (answers ?? Enumerable.Empty<SubmittedAnswer>()).ToList();
        }
    }

    public class SubmittedAnswer
    {
        public int QuestionId { get; }
        public int SelectedOptionId { get; }

        public SubmittedAnswer(int questionId, int selectedOptionId)
        {
            QuestionId = questionId;
            SelectedOptionId = selectedOptionId;
        }
    }

    public class CompetencyScore
    {
        public int CompetencyId { get; }
        public int Correct { get; }

        public CompetencyScore(int competencyId, int correct)
        {
            CompetencyId = competencyId;
            Correct = correct;
        }
    }

    public class EvaluationResult
    {
        public string CandidateId { get; }
        public string EvaluationId { get; }
        public int AnswersReceived { get; }
        public int EvaluatedAnswers { get; }
        public int CorrectAnswers { get; }
        public IReadOnlyCollection<CompetencyScore> CompetencyScores { get; }
        public string? CompletionRate { get; }
        public string? Timestamp { get; }

        public EvaluationResult(
            string candidateId,
            string evaluationId,
            int answersReceived,
            int evaluatedAnswers,
            int correctAnswers,
            IEnumerable<CompetencyScore> competencyScores,
            string? completionRate,
            string? timestamp)
        {
            CandidateId = candidateId ?? string.Empty;
            EvaluationId = evaluationId ?? string.Empty;
            AnswersReceived = answersReceived;
            EvaluatedAnswers = evaluatedAnswers;
            CorrectAnswers = correctAnswers;
            CompetencyScores = (competencyScores ?? Enumerable.Empty<CompetencyScore>()).ToList();
            CompletionRate = completionRate;
            Timestamp = timestamp;
        }
    }
}

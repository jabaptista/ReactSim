using System.Collections.Generic;

namespace ReactSim.DTO.Evaluation
{
    public class SubmitEvaluationResponse
    {
        public string Message { get; set; } = string.Empty;
        public string? CandidateId { get; set; }
        public string? EvaluationId { get; set; }
        public int AnswersReceived { get; set; }
        public int EvaluatedAnswers { get; set; }
        public int CorrectAnswers { get; set; }
        public string? CompletionRate { get; set; }
        public string? Timestamp { get; set; }
        public List<CompetencyScoreResponse> CompetencyScores { get; set; } = new();
    }

    public class CompetencyScoreResponse
    {
        public int CompetencyId { get; set; }
        public int Correct { get; set; }
    }
}

using System.Collections.Generic;

namespace ReactSim.Repositories.dbo
{
    public class EvaluationResult : MongoEntity
    {
        public string? CandidateId { get; set; }
        public string? EvaluationId { get; set; }
        public int AnswersReceived { get; set; }
        public int EvaluatedAnswers { get; set; }
        public int CorrectAnswers { get; set; }
        public string? CompletionRate { get; set; }
        public string? Timestamp { get; set; }
        public IEnumerable<CompetencyScore> CompetencyScores { get; set; } = new List<CompetencyScore>();
    }
}

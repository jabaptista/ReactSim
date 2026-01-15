using System.Collections.Generic;

namespace ReactSim.DTO.Evaluation
{
    public class SubmitEvaluationRequest
    {
        public string? CandidateId { get; set; }
        public string? EvaluationId { get; set; }
        public string? Timestamp { get; set; }
        public List<SubmittedAnswer> Answers { get; set; } = new();
        public string? CompletionRate { get; set; }
    }

    public class SubmittedAnswer
    {
        public string? QuestionId { get; set; }
        public string? SelectedOptionId { get; set; }
    }
}

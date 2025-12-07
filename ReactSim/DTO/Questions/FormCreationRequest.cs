namespace ReactSim.DTO.Questions
{
    public class FormCreationRequest
    {
        public string Scenario { get; set; }
        public IEnumerable<Question> Questions { get; set; } = Enumerable.Empty<Question>();

        public int TotalQuestions { get; set; }
    }
}

namespace ReactSim.DTO.Questions
{
    public class FormCreationRequest
    {
        public string Scenario { get; set; }
        public IEnumerable<Question> Questions { get; set; }

        public int TotalQuestions { get; set; }
    }
}

namespace ReactSim.DTO.Questions
{
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> Competencies { get; set; }
        
        public IEnumerable<AwnserOption> Options { get; set; }

        public IEnumerable<string>? MediaURL { get; set; }

        public int RightAwnser { get; set; }
    }
}

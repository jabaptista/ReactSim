using MongoDB.Driver;

namespace ReactSim.Repositories.dbo
{
    public class Question : MongoEntity
    {
        public string Description { get; set; }
        public IEnumerable<int> Competencies { get; set; }

        public IEnumerable<AwnserOption> Options { get; set; }

        public IEnumerable<string>? MediaURL { get; set; }

        public int RightAwnser { get; set; }
        
    }
}

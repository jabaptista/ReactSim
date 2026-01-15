using System.Collections.Generic;

namespace ReactSim.Repositories.dbo
{
    public class Activity : MongoEntity
    {
        public string ActivityId { get; set; }
        public int Status { get; set; }
        public IEnumerable<string> Participants { get; set; } = new List<string>();
    }
}

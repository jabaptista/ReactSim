namespace ReactSim.DTO.Analytics
{
    public class AnalyticsDefinition
    {
        public string name { get;  private set; }
        public string type { get; private set; }

        public AnalyticsDefinition(String name, String type)
        {
            this.name = name;
            this.type = type;
        }
    }
}

namespace ReactSim.DTO.Activity
{
    public class ActivityQuestion
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public ActivityMedia media { get; set; }
        public List<ActivityOption> options { get; set; }
    }

    public class ActivityMedia
    {
        public string type { get; set; }
        public string url { get; set; }
        public string caption { get; set; }
        public List<ActivityMediaItem> items { get; set; }
    }

    public class ActivityMediaItem
    {
        public string url { get; set; }
        public string caption { get; set; }
    }

    public class ActivityOption
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}

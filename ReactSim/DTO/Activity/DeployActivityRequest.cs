namespace ReactSim.DTO.Activity
{
    public class DeployActivityRequest
    {
        public string activityID {get;set;}
        public string InventRAstdID { get; set; }
        public Dictionary<string, string> json_params { get; set; }
    }
}

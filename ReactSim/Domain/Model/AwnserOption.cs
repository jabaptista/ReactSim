namespace ReactSim.Domain.Model
{
    public class AwnserOption
    {
        public int Id { get; }
        public string Text { get; }
        
        public AwnserOption(int id, string text)
        {
            Text = text;
            Id = id;
        }
    }
}

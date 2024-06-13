namespace RunningApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Distance { get; set; } = string.Empty;
        
        public string OrganizerId { get; set; } = string.Empty;
        public ICollection<Result> Results { get; set; }
    }
}
namespace RunningApp.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public TimeSpan? Time { get; set; }
    }
}
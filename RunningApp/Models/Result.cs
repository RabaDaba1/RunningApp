namespace RunningApp.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int EventId { get; set; }
        public TimeSpan? Time { get; set; }
        public  virtual Event? Event { get; set; }
        public virtual Athlete? Athlete { get; set; }
    }
}
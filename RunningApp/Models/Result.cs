using RunningApp.Areas.Identity.Data;

namespace RunningApp.Models
{
    public class Result
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public TimeSpan Time { get; set; }
        public  virtual Event? Event { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
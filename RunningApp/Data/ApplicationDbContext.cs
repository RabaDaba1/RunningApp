using Microsoft.EntityFrameworkCore;
using RunningApp.Models;

namespace RunningApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Event> Events { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
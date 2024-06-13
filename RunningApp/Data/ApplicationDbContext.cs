using Microsoft.EntityFrameworkCore;
using RunningApp.Areas.Identity.Data;
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
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure composite key
            modelBuilder.Entity<Result>()
                .HasKey(r => new { r.UserId, r.EventId });

            // Configure unique constraints
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => new { u.FirstName, u.LastName })
                .IsUnique();

            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Name)
                .IsUnique();

            // Konfiguracja kluczy obcych
            modelBuilder.Entity<Result>()
                .HasOne(r => r.User)
                .WithMany(a => a.Results)
                .HasForeignKey(r => r.UserId) // Correct
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Results)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Event>().HasData(
            //     new Event
            //     {
            //         Date = "19.08.2023", Distance = "10 km", Id = 1, Location = "Warszawa", Name = "Warszawska Dycha"
            //     },
            //     new Event
            //     {
            //         Date = "29.08.2023", Distance = "21 km", Id = 2, Location = "Warszawa",
            //         Name = "Półmaraton Warszawski"
            //     },
            //     new Event
            //     {
            //         Date = "31.06.2022", Distance = "42 km", Id = 3, Location = "Warszawa", Name = "Maraton Krakowski"
            //     },
            //     new Event
            //     {
            //         Date = "09.07.2021", Distance = "5 km", Id = 4, Location = "Warszawa", Name = "Zabawna Piątka"
            //     }
            // );
            //
            // modelBuilder.Entity<Result>().HasData(
            //     new Result { Id = 1, UserId = "1", EventId = 1, Time = new TimeSpan(0, 25, 30) },
            //     new Result { Id = 2, UserId = "2", EventId = 1, Time = new TimeSpan(0, 24, 10) },
            //     new Result { Id = 3, UserId = "3", EventId = 2, Time = new TimeSpan(0, 30, 45) },
            //     new Result { Id = 4, UserId = "4", EventId = 2, Time = new TimeSpan(0, 27, 15) },
            //     new Result { Id = 5, UserId = "5", EventId = 3, Time = new TimeSpan(0, 29, 50) }
            // );
        }
    }
}
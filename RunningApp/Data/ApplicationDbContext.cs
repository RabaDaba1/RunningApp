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
        public DbSet<Athlete> Athletes { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja kluczy obcych
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Athlete)
                .WithMany(a => a.Results)
                .HasForeignKey(r => r.AthleteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Results)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<Event>().HasData(
                new Event { Date = "19.08.2023", Distance = "10 km", Id = 1, Location = "Warszawa", Name = "Warszawska Dycha" },
                new Event { Date = "29.08.2023", Distance = "21 km", Id = 2, Location = "Warszawa", Name = "Półmaraton Warszawski"},
                new Event { Date = "31.06.2022", Distance = "42 km", Id = 3, Location = "Warszawa", Name = "Maraton Krakowski"},
                new Event { Date = "09.07.2021", Distance = "5 km", Id = 4, Location = "Warszawa", Name = "Zabawna Piątka"}
                );
            modelBuilder.Entity<Athlete>().HasData(
                new Athlete { Id = 1, FirstName = "Arek", LastName = "Kowalski",  DateOfBirth = new DateTime(1990, 1, 1) },
                new Athlete { Id = 2, FirstName = "Kiptum", LastName = "Kowalski",  DateOfBirth = new DateTime(1992, 1, 1) },
                new Athlete { Id = 3, FirstName = "Maciek", LastName = "Kowalski",  DateOfBirth = new DateTime(1994, 1, 1) },
                new Athlete { Id = 4, FirstName = "Kacper", LastName = "Kowalski",  DateOfBirth = new DateTime(1996, 1, 1) },
                new Athlete { Id = 5, FirstName = "Przemek", LastName = "Kowalski",  DateOfBirth = new DateTime(1998, 1, 1) }
            );
            modelBuilder.Entity<Result>().HasData(
                new Result { Id = 1, AthleteId = 1, EventId = 1, Time = new TimeSpan(0, 25, 30) },
                new Result { Id = 2, AthleteId = 2, EventId = 1, Time = new TimeSpan(0, 24, 10) },
                new Result { Id = 3, AthleteId = 3, EventId = 2, Time = new TimeSpan(0, 30, 45) },
                new Result { Id = 4, AthleteId = 4, EventId = 2, Time = new TimeSpan(0, 27, 15) },
                new Result { Id = 5, AthleteId = 5, EventId = 3, Time = new TimeSpan(0, 29, 50) }
            );
        }
    }
        
        
}
    
    

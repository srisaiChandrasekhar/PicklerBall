using Microsoft.EntityFrameworkCore;

namespace Picklr.Models
{
    public class PicklrContext : DbContext
    {
        public PicklrContext(DbContextOptions<PicklrContext> options) : base(options) { }

        public DbSet<Club> Clubs { get; set; } = null!;
        public DbSet<PicklProgram> Programs { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Clubs
            modelBuilder.Entity<Club>().HasData(
                new Club
                {
                    ClubID = 1,
                    Name = "Picklr Downtown",
                    Location = "123 Main St, Chicago, IL",
                    Description = "Our flagship downtown club with 10 indoor courts."
                },
                new Club
                {
                    ClubID = 2,
                    Name = "Picklr Northside",
                    Location = "456 Oak Ave, Evanston, IL",
                    Description = "A vibrant outdoor facility with 8 courts and a pro shop."
                }
            );

            // Seed Programs (with ClubID and AvailableDays)
            modelBuilder.Entity<PicklProgram>().HasData(
                new PicklProgram
                {
                    ProgramID = 1,
                    Name = "Beginner Open Play",
                    Description = "Drop-in open play for new players. No experience needed.",
                    Fee = 10.00m,
                    ClubID = 1,
                    AvailableDays = Weekdays.Monday | Weekdays.Wednesday | Weekdays.Friday
                },
                new PicklProgram
                {
                    ProgramID = 2,
                    Name = "Intermediate Clinic",
                    Description = "Weekly skill-building clinic led by a certified coach.",
                    Fee = 25.00m,
                    ClubID = 1,
                    AvailableDays = Weekdays.Tuesday | Weekdays.Thursday
                },
                new PicklProgram
                {
                    ProgramID = 3,
                    Name = "Advanced Tournament",
                    Description = "Competitive round-robin tournament for rated players.",
                    Fee = 40.00m,
                    ClubID = 2,
                    AvailableDays = Weekdays.Saturday | Weekdays.Sunday
                }
            );

            // Seed Users
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    UserID = 1,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice@picklr.com",
                    Role = "Admin"
                },
                new AppUser
                {
                    UserID = 2,
                    FirstName = "Bob",
                    LastName = "Jones",
                    Email = "bob@picklr.com",
                    Role = "Client"
                }
            );
        }
    }
}

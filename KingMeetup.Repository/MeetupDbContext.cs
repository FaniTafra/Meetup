using KingMeetup.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KingMeetup.Repository
{
    public class MeetupDbContext : DbContext
    {
        public MeetupDbContext(DbContextOptions<MeetupDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UsersInterest> UsersInterests { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<AttendeeList> AttendeeLists { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<EventReview> EventReviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}

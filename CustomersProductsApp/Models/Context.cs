using Microsoft.EntityFrameworkCore;

namespace TraineesApp.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Trainee> Trainees { get; set; }
    }
}

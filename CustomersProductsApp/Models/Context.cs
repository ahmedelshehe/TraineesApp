using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TraineesApp.Viewmodels;

namespace TraineesApp.Models
{
    public class Context : IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<TraineesApp.Viewmodels.RegisterModel>? RegisterModel { get; set; }

        public DbSet<TraineesApp.Viewmodels.LoginModel>? LoginModel { get; set; }
    }
}

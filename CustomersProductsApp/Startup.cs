using Microsoft.EntityFrameworkCore;
using TraineesApp.Models;
using TraineesApp.Repositories;

namespace TraineesApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment webHost)
        {
            Configuration = configuration;
            WebHost = webHost;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHost { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITraineeRepository, TraineeRepository>();

            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app)
        {

            if (!WebHost.IsEnvironment("Development"))
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using TraineesApp.Models;

namespace TraineesApp.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        public IEnumerable<Course>? GetAllCoursesWithTrack();
        public Course? GetCourseWithTrack(int id);
    }
}

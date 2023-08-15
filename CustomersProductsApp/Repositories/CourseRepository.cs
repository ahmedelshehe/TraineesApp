using Microsoft.EntityFrameworkCore;
using TraineesApp.Models;

namespace TraineesApp.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public readonly Context _context;
        public CourseRepository(Context context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Course>? GetAllCoursesWithTrack()
        {
            return _context.Courses.Include(c => c.Track).ToList();
        }

        public Course? GetCourseWithTrack(int id)
        {
            return _context.Courses.Include(c => c.Track).FirstOrDefault(c => c.Id == id);
        }
    }
}

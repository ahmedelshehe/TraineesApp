using TraineesApp.Models;

namespace TraineesApp.Repositories
{
    public interface ITraineeRepository : IRepository<Trainee>
    {
        public IEnumerable<Trainee> GetAllTraineesWithTrack();
        public Trainee? GetTraineeByIdWithTrack(int id);
    }
}

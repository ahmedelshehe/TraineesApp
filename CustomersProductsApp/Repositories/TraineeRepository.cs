using Microsoft.EntityFrameworkCore;
using TraineesApp.Models;

namespace TraineesApp.Repositories
{
	public class TraineeRepository : Repository<Trainee>, ITraineeRepository
	{
		private readonly Context _context;
		public TraineeRepository(Context context) : base(context)
		{
			_context = context;
		}

		public IEnumerable<Trainee> GetAllTraineesWithTrack()
		{
			return _context.Trainees.Include(t => t.Track).ToList();
		}

		public Trainee? GetTraineeByIdWithTrack(int id)
		{
			return _context.Trainees.Include(t => t.Track).FirstOrDefault(t => t.Id == id);
		}
	}
}

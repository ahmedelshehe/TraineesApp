using TraineesApp.Models;

namespace TraineesApp.Repositories
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(Context context) : base(context)
        {
        }
    }
}

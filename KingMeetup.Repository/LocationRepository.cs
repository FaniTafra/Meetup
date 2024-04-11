using KingMeetup.Model;
using KingMeetup.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KingMeetup.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MeetupDbContext _context;
        public LocationRepository(MeetupDbContext context)
        {
               _context = context;
        }
        public async Task<List<State>> GetAllStates(CancellationToken cancellationToken)
        {
            return await _context.States.Where(s => s.Active).OrderBy(s => s.Name).ToListAsync(cancellationToken);
        }

        public async Task<List<City>> GetCitiesInState(int Id, CancellationToken cancellationToken)
        {
            return await _context.Cities.Where(c =>c.StateId == Id && c.Active).OrderBy(c => c.Name).ToListAsync(cancellationToken);
        }
    }
}

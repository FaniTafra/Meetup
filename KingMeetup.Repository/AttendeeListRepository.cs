using KingMeetup.Model;
using KingMeetup.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using KingMeetup.Model.Enums;

namespace KingMeetup.Repository
{
    public class AttendeeListRepository : IAttendeeListRepository
    {
        private readonly MeetupDbContext _context;

        public AttendeeListRepository(MeetupDbContext context)
        {
            _context = context;
        }
        public async Task EventSignup(AttendeeList attendeeList, CancellationToken cancellationToken)
        {
            await _context.AttendeeLists.AddAsync(attendeeList, cancellationToken);
        }
        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<AttendeeList> GetByEventAndUserId(int eventId, int userId,CancellationToken cancellationToken)
        {
            return await _context.AttendeeLists.FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId, cancellationToken);
        }
        public async Task<List<AttendeeList>> GetEventAttendeesByEventId(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.AttendeeLists.Where(x => x.EventId == id).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AttendeeList> GetAttendeeListWithEvent(int eventId,int userId, CancellationToken cancellationToken)
        {
            return await _context.AttendeeLists.Include(a => a.Event).FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == userId);
        }
        public async Task<int> GetNumberOfSignedUpOnSite(int id,bool IsOnSite ,CancellationToken cancellationToken) 
        {
            return await _context.AttendeeLists.Where(x => x.EventId == id && x.IsOnSite && x.Active && x.StatusId == AttendeeStatus.In).CountAsync(cancellationToken);
        }
        public async Task<int> GetNumberOfSignedUpOnLine(int id, bool IsOnSite, CancellationToken cancellationToken)
        {
            return await _context.AttendeeLists.Where(x => x.EventId == id && !x.IsOnSite && x.Active && x.StatusId == AttendeeStatus.In).CountAsync(cancellationToken);
        }
    }
}

using KingMeetup.Model;
using KingMeetup.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace KingMeetup.Repository
{
    public class InterestRepository : IInterestRepository
    {
        private readonly MeetupDbContext _context;

        public InterestRepository(MeetupDbContext context)
        {
            _context = context;
        }
        public async Task<List<Interest>> GetInterests(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Interests.Where(i => i.Active).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddUsersInterestAsync(List<UsersInterest> usersInterests, CancellationToken cancellationToken)
        {
            await _context.UsersInterests.AddRangeAsync(usersInterests, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> CheckUsersInterest(int userId)
        {
            return await _context.UsersInterests.AnyAsync(ui => ui.UserId == userId);
        }

        public async Task<List<Interest>> GetUserInterests(int userId)
        {
            try
            {
                return await _context.Interests.Where(i => i.Active).Join(_context.UsersInterests.Where(ui => ui.UserId == userId), i => i.Id, ui => ui.InterestId, (i, ui) => i).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

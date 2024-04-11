using KingMeetup.Model;
using KingMeetup.Model.IRepository;
using Microsoft.EntityFrameworkCore;

namespace KingMeetup.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MeetupDbContext _context;

        public AuthRepository(MeetupDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

         public  async Task<User?> FindByEmailAndPassword(string email, string password, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<User?> FindByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

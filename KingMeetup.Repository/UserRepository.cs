using KingMeetup.Model;
using KingMeetup.Model.IRepository;
using KingMeetup.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMeetup.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MeetupDbContext _context;

        public UserRepository(MeetupDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int userId)
        {
            try
            {
                User user = await _context.Users
                    .Include(u => u.Certificates)
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();
                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(User user, int UserId)
        {
            User? updatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (updatedUser != null)
            {
                updatedUser.FirstName = user.FirstName;
                updatedUser.LastName = user.LastName;
                if(user.Password != null)
                { 
                    updatedUser.Password = user.Password;
                }
                
                updatedUser.Phone = user.Phone;
                updatedUser.Email = user.Email;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMeetup.Model.Repositories
{
    public interface IInterestRepository
    {
        Task<List<Interest>> GetInterests(CancellationToken cancellationToken);
        Task AddUsersInterestAsync(List<UsersInterest> usersInterests, CancellationToken cancellationToken);
        Task<bool> CheckUsersInterest(int userId);
        Task<List<Interest>> GetUserInterests(int userId);
    }
}

using KingMeetup.Messaging;

namespace KingMeetup.Contract
{
    public interface IInterestService
    {
        Task<List<InterestResponse>> GetInterests(CancellationToken cancellationToken);
        Task AddUserInterest(List<UsersInterestRequest> request, CancellationToken cancellationToken);
        Task<bool> CheckUsersInterest(int userId);
        Task<List<InterestResponse>> GetUserInterests(int userId);
    }
}

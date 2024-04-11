using KingMeetup.Messaging;

namespace KingMeetup.Blazor.IService
{
    public interface IInterestsService
    {
        Task<List<InterestResponse>> GetInterests();
        Task<int> GetUserId();
        Task AddUsersInterests(List<UsersInterestRequest> usersInterestRequest);
        Task<bool> CheckUsersInterest();
        Task<List<InterestResponse>> GetUserInterests();
    }
}

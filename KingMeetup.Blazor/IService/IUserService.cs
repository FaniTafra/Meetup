using KingMeetup.Messaging;

namespace KingMeetup.Blazor.IService
{
    public interface IUserService
    {
        Task<UserUpdateResponse> GetUser();
        Task UpdateUser(UserUpdateResponse updateResponse);
    }
}

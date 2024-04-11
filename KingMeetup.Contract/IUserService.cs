using KingMeetup.Messaging;

namespace KingMeetup.Contract
{
    public interface IUserService
    {
        Task <UserResponse>UpdateUser(int userId, UserUpdateRequest request);
        Task<UserUpdateResponse> GetUser(int userId);
    }
}

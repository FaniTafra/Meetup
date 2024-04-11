using KingMeetup.Messaging;

namespace KingMeetup.Blazor.IService
{
    public interface IAuthService
    {
        Task Login(LoginRequest userDto);
        Task AddUserAsync(UserRequest user);
    }
}

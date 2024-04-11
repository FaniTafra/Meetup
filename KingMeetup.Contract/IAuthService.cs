using KingMeetup.Messaging;
using KingMeetup.Model;

namespace KingMeetup.Contract
{
    public interface IAuthService
    {
        Task<LoginResponse> Generate(LoginRequest user, CancellationToken cancellationToken);
        Task<User?> Authenticate(LoginRequest userDto, CancellationToken cancellationToken);
        Task<UserResponse> RegisterUser(UserRequest request, CancellationToken cancellationToken);
    }
}

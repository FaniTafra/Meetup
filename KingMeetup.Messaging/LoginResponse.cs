using KingMeetup.Messaging.Common;

namespace KingMeetup.Messaging
{
    public class LoginResponse : ResponseBase<LoginRequest>
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}

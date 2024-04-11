using KingMeetup.Messaging.Common;

namespace KingMeetup.Messaging
{
    public class UserResponse : ResponseBase<UserRequest>
    {
        public int Id { get; set; }
    }
}

using KingMeetup.Messaging.Common;

namespace KingMeetup.Messaging
{
    public class AttendeeListsResponse : ResponseBase<AttendeeListsRequest>
    {
        public int id { get; set; }
        public bool IsSignedUp { get; set; }
    }
}

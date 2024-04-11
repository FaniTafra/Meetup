using KingMeetup.Messaging.Common;

namespace KingMeetup.Messaging
{
    public class AttendeeListsRequest : RequestBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int StatusId { get; set; } = 0;
        public bool IsOnSite { get; set; }
    }
}

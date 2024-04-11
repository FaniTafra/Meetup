using KingMeetup.Messaging.Common;

namespace KingMeetup.Messaging
{
    public class EventResponse : ResponseBase<EventRequest>
    {
        public int id { get; set; }
        public bool IsSignedUp { get; set; }
    }
}

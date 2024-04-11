using KingMeetup.Messaging.Common;

namespace KingMeetup.Messaging
{
    public class CityRequest : RequestBase
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int stateId { get; set; }
    }
}

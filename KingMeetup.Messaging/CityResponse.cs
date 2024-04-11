using KingMeetup.Messaging.Common;

namespace KingMeetup.Messaging
{
    public class CityResponse : ResponseBase<RequestBase>
    {
        public int Id { get; set; }
        public List<CityRequest> Cities { get; set; }
        
    }
}

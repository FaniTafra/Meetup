using KingMeetup.Messaging.Common;


namespace KingMeetup.Messaging
{
    public class StateResponse : RequestBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}

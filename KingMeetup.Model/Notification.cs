namespace KingMeetup.Model
{
    public class Notification : AuditableEntity
	{
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string Message { get; set; }
    }
}

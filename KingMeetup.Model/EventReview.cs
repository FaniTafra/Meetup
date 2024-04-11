namespace KingMeetup.Model
{
    public class EventReview : AuditableEntity
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public byte Review { get; set; }
        public string Comment { get; set; }
    }
}

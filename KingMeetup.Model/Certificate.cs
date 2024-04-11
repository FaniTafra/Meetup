namespace KingMeetup.Model
{
    public class Certificate : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event  { get; set; }
        public string FileLocation { get; set; }
    }
}

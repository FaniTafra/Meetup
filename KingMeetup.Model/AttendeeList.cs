using KingMeetup.Model.Enums;

namespace KingMeetup.Model
{
    public class AttendeeList : AuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public bool IsOnSite { get; set; }
        public AttendeeStatus StatusId { get; set; }
    }
}

using KingMeetup.Model.Enums;

namespace KingMeetup.Model
{
    public class Event : AuditableEntity
	{
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
        public int AttendeesOnSite { get; set; }
        public int AttendeesOnLine { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public List<EventReview> EventReviews { get; set; } = new List<EventReview>();
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public List<AttendeeList> AttendeeLists { get; set; } = new List<AttendeeList>();
    }
}

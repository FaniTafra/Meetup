using KingMeetup.Model.Enums;
using System.Text.Json.Serialization;

namespace KingMeetup.Model
{
    public class User : AuditableEntity
	{
        public int Id { get; set; }
        public Status Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public Role RoleID { get; set; }
        public string? Description { get; set; }
        public string? PictureLocation { get; set; }
        public List<UsersInterest> UsersInterests { get; set; } = new List<UsersInterest>();
        [JsonIgnore]
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<AttendeeList> AttendeeLists { get; set; } = new List<AttendeeList>();
        public List<EventReview> EventReviews { get; set; } = new List<EventReview>();
    }
}
namespace KingMeetup.Model
{
    public class UsersInterest : AuditableEntity
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}

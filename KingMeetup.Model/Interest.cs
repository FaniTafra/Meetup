namespace KingMeetup.Model
{
    public class Interest : AuditableEntity
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UsersInterest> UsersInterests { get; set; } = new List<UsersInterest>();
        public List<Event> Events { get; set; } = new List<Event>();
    }
}

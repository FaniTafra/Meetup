namespace KingMeetup.Model
{
    public class State : AuditableEntity
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}

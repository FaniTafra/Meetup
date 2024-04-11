namespace KingMeetup.Model
{
    public class City : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }
}

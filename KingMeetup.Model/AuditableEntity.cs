namespace KingMeetup.Model
{
	public abstract class AuditableEntity
	{
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? DateModified { get; set; }
		public string? ModifiedBy { get; set; }
	}
}

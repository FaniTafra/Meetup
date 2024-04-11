namespace KingMeetup.Model.Repositories
{
    public interface IAttendeeListRepository
    {
        Task<List<AttendeeList>> GetEventAttendeesByEventId(int id, CancellationToken cancellationToken);
        Task EventSignup(AttendeeList attendeeList, CancellationToken cancellationToken);
        Task<int> GetNumberOfSignedUpOnLine(int id, bool IsOnSite, CancellationToken cancellationToken);
        Task<int> GetNumberOfSignedUpOnSite(int id, bool IsOnSite, CancellationToken cancellationToken);
        Task<AttendeeList> GetByEventAndUserId(int eventId, int userId, CancellationToken cancellationToken);
        Task Save(CancellationToken cancellationToken);
        Task<AttendeeList> GetAttendeeListWithEvent(int eventId,int userId, CancellationToken cancellationToken);
    }
}

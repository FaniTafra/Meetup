namespace KingMeetup.Model.Repositories
{
    public interface IEventRepository
    {
        Task Create(Event newEvent, CancellationToken cancellationToken);
        Task<Event> FindById (int id, CancellationToken cancellationToken);
        Task<List<Event?>> GetUserEvents(int userId);
        Task<bool> Update(Event newEvent, CancellationToken cancellationToken);
        Task<List<Event?>> GetAdminEvents(int userId);
        Task<List<Event?>> GetSignedUpEvents(int userId);
    }
}

namespace KingMeetup.Model.Repositories
{
    public interface ILocationRepository
    {
        Task<List<State>> GetAllStates(CancellationToken cancellationToken);

        Task<List<City>> GetCitiesInState(int Id, CancellationToken cancellationToken);
    }
}

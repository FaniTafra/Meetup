using KingMeetup.Messaging;

namespace KingMeetup.Contract
{
    public interface ILocationService
    {
        Task<List<StateResponse>> GetStates(CancellationToken cancellationToken);
        Task<CityResponse> GetCities(CityRequest request, CancellationToken cancellationToken);
    }
}

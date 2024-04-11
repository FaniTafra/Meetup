using KingMeetup.Messaging;

namespace KingMeetup.Blazor.IService
{
    public interface ILocationService
    {
        Task<List<StateResponse>> GetStates();
        Task<List<CityRequest>> GetCities(int id);
    }
}

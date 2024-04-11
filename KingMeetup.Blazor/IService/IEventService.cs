using KingMeetup.Messaging;

namespace KingMeetup.Blazor.IService
{
    public interface IEventService
    {
        Task<EventResponse> Create (EventRequest eventRequest);
        Task<EventResponse> Get(int eventId);
        Task<AttendeeListsResponse> IsUserSignedUp(int eventId,int userId);
        Task<int> GetUserId();
        Task<AttendeeListsResponse> SignUp(int eventId,int userId,bool onSite);
        Task<AttendeeListsResponse> SignOff(int eventId, int userId);
        Task<List<EventResponse>> GetUserEvents();
        Task<EventResponse> Update(EventRequest eventRequest);
        Task<List<EventResponse>> GetAdminEvents();
        Task<List<EventResponse>> GetSignedUpEvents();
    }
}

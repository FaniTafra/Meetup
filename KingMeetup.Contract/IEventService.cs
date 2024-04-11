using KingMeetup.Messaging;
using KingMeetup.Model;

namespace KingMeetup.Contract
{
    public interface IEventService
    {
        Task<EventResponse> Create (EventRequest request, CancellationToken cancellationToken);
        Task<AttendeeListsResponse> SignUp(AttendeeListsRequest request, CancellationToken cancellationToken);
        Task<EventResponse> FindById(EventRequest request, CancellationToken cancellationToken);
        Task<AttendeeListsResponse> SignOff(AttendeeListsRequest request, CancellationToken cancellationToken);
        Task<AttendeeListsResponse> IsUserSignedUp(AttendeeListsRequest request, CancellationToken cancellationToken);
        Task<List<EventResponse>> GetUserEvents(int userId);
        Task<EventResponse> Update(EventRequest request, CancellationToken cancellationToken);
        Task<List<EventResponse>> GetAdminEvents(int userId);
        Task<List<EventResponse>> GetSignedUpEvents(int userId);
    }
}

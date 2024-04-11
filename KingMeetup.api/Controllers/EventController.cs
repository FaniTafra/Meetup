using KingMeetup.API.Common;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingMeetup.API.Controllers
{
    public class EventController : BaseApiController
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest request, CancellationToken cancellationToken)
        {
            EventResponse response = await _eventService.Create(request, cancellationToken);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] EventRequest request, CancellationToken cancellationToken)
        {
            EventResponse response = await _eventService.Update(request, cancellationToken);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
        [HttpPost("Signup")]
        public async Task<IActionResult> EventSignup([FromBody] AttendeeListsRequest request, CancellationToken cancellationToken)
        {
            AttendeeListsResponse response = await _eventService.SignUp(request, cancellationToken);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
        [HttpPost("Signoff")]
        public async Task<IActionResult> EventSignoff([FromBody] AttendeeListsRequest request, CancellationToken cancellationToken)
        {
            AttendeeListsResponse response = await _eventService.SignOff(request, cancellationToken);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetEvent:{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id,CancellationToken cancellationToken)
        {
            EventRequest req = new EventRequest() { Id = id };
            EventResponse res = await _eventService.FindById(req, cancellationToken);
            if (res.Success)
                return Ok(res);
            return BadRequest(res);           
        }
        [HttpGet("IsSignedUp")]
        public async Task<IActionResult> IsSignedIn([FromQuery] int eventId, [FromQuery] int userId, CancellationToken cancellationToken)
        {
            AttendeeListsRequest req = new AttendeeListsRequest() { EventId = eventId, UserId = userId };
            AttendeeListsResponse res = await _eventService.IsUserSignedUp(req, cancellationToken);
            if (res.Success)
                return Ok(res);
            return BadRequest(res);
        }

        [HttpGet("GetUserEvents/{userId}")]
        public async Task<IActionResult> GetUserEvents(int userId)
        {
            try
            {
                return Ok(await _eventService.GetUserEvents(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAdminEvents/{userId}")]
        public async Task<IActionResult> GetAdminEvents(int userId)
        {
            try
            {
                return Ok(await _eventService.GetAdminEvents(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetSignedUpEvents/{userId}")]
        public async Task<IActionResult> GetSignedUpEvents(int userId)
        {
            try
            {
                return Ok(await _eventService.GetSignedUpEvents(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

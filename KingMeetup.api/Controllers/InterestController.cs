using KingMeetup.API.Common;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace KingMeetup.API.Controllers
{
    public class InterestController : BaseApiController
    {
        private readonly IInterestService _interestService;

        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpGet("GetInterests")]
        public async Task<IActionResult> GetInterests(CancellationToken cancellationToken)
        {
            try
            {
                List<InterestResponse> interestResponses = await _interestService.GetInterests(cancellationToken);
                return Ok(interestResponses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("AddUserInterests")]
        public async Task<IActionResult> AddUserInterests([FromBody] List<UsersInterestRequest> userInterestRequest, CancellationToken cancellationToken)
        {
            try
            {
                await _interestService.AddUserInterest(userInterestRequest, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }

        [HttpGet("Check/{userId}")]
        public async Task<IActionResult> CheckUsersInterest(int userId)
        {
            try
            {
                return Ok(await _interestService.CheckUsersInterest(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetUserInterests/{userId}")]
        public async Task<IActionResult> GetUserInterests(int userId)
        {
            try
            {
                return Ok(await _interestService.GetUserInterests(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

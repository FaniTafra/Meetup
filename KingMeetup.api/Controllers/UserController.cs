using KingMeetup.API.Common;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace KingMeetup.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(int userId, UserUpdateRequest updateRequest)
        {
            try
            {
                UserResponse response = await _userService.UpdateUser(userId, updateRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                UserUpdateResponse response = await _userService.GetUser(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

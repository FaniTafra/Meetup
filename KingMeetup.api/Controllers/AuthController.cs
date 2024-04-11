using KingMeetup.API.Common;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingMeetup.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            LoginResponse response = await _authService.Generate(request, cancellationToken);
            if(response.Success) 
                return Ok(response.Token);

            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<IActionResult> AddUser([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            UserResponse response = await _authService.RegisterUser(request, cancellationToken);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

    }
}

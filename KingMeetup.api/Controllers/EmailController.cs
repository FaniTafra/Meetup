using KingMeetup.API.Common;
using KingMeetup.Contract;
using KingMeetup.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace KingMeetup.API.Controllers
{
    public class EmailController : BaseApiController
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost("Send")]
        public IActionResult SendEmail(EmailRequest request)
        {
            EmailResponse res = _emailService.SendEmail(request);
            if(res.Success)
                return Ok(res);
            return BadRequest(res);
        }
    }
}

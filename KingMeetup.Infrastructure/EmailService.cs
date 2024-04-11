using KingMeetup.Contract;
using KingMeetup.Messaging;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace KingMeetup.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public EmailResponse SendEmail(EmailRequest request)
        {
            EmailResponse response = new EmailResponse();
            try 
            {
                var emailSection = _config.GetSection("Email");
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(emailSection["EmailUserName"]));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(emailSection["EmailHost"], 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailSection["EmailUserName"], emailSection["EmailPassword"]);
                smtp.Send(email);
                smtp.Disconnect(true);

                response.Success = true;
                response.Message = "Mail poslan";

                return response;
            }
            catch(Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}

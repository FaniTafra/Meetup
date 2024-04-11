using KingMeetup.Messaging;

namespace KingMeetup.Contract
{
    public interface IEmailService
    {
        EmailResponse SendEmail(EmailRequest request);
    }
}

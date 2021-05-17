using TAO_Backend.Models;

namespace TAO_Backend.Services
{
    public interface IEmailService
    {
        bool SendEmail(EmailData emailData);
    }
}

using Microsoft.Extensions.Configuration;

namespace EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailServiceMessage message);
    }
}

namespace EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailServiceMessage message);
    }
}
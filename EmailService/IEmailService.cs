namespace EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailServiceMessage message);
    }
}
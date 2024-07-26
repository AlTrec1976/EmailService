using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly string _smtpName;
        private readonly string _smtpPass;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _smtpName = configuration.GetSection("SmtpConnect:Name").Value;
            _smtpPass = configuration.GetSection("SmtpConnect:Password").Value;
        }

        public async Task SendEmailAsync(EmailServiceMessage message)
        {
            try
            {
                MailAddress _from = new MailAddress(message.EmailFrom, "Alex");
                MailAddress _to = new MailAddress(message.EmailTo);
                MailMessage _message = new MailMessage(_from, _to);
                _message.Subject = "Регистрация прошла успешно";
                _message.Body = message.MessageBody;
                SmtpClient smtp = new SmtpClient("mail.altrec.ru", 587);
                smtp.Credentials = new NetworkCredential(_smtpName, _smtpPass);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(_message);
                Console.WriteLine("Письмо отправлено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка отправки письма в SendEmailAsync");
                throw;
            }
        }
    }
}

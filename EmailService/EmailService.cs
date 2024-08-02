using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly string _smtpHost;
        private readonly string _smtpPort;
        private readonly string _smtpName;
        private readonly string _smtpPass;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _smtpHost = configuration.GetSection("SmtpConnect:Host").Value;
            _smtpPort = configuration.GetSection("SmtpConnect:Port").Value;
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
                SmtpClient smtp = new SmtpClient(_smtpHost, int.Parse(_smtpPort));
                smtp.Credentials = new NetworkCredential(_smtpName, _smtpPass);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(_message);
                _logger.LogInformation($"{DateTime.Now} Письмо {message.EmailTo} отправлено успешно");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} Ошибка отправки письма {message.EmailTo}");
                throw;
            }
        }
    }
}

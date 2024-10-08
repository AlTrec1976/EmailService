﻿using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using EmailService.Entity;
using Microsoft.Extensions.Options;

namespace EmailServices
{
    public class EmailSendService : IEmailService
    {
        private readonly ILogger<EmailSendService> _logger;
        private readonly IOptions<SmtpConnect> _options;

        public EmailSendService(ILogger<EmailSendService> logger, IOptions<SmtpConnect> options)
        {
            _logger = logger;
            _options = options;
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
                SmtpClient smtp = new SmtpClient(_options.Value.Host, _options.Value.Port);
                smtp.Credentials = new NetworkCredential(_options.Value.Name, _options.Value.Password);
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

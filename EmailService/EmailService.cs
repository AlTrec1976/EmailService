using System.Net.Mail;
using System.Net;

namespace EmailService
{
    public class EmailService
    {
        public async Task SendEmailAsync(EmailServiceMessage message)
        {
            MailAddress _from = new MailAddress(message.EmailFrom, "Alex");
            MailAddress _to = new MailAddress(message.EmailTo);
            MailMessage _message = new MailMessage(_from, _to);
            _message.Subject = "Тест";
            _message.Body = message.MessageBody;
            SmtpClient smtp = new SmtpClient("mail.altrec.ru", 587);
            smtp.Credentials = new NetworkCredential("somemail@altrec.ru", "fgjh456gfjh");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(_message);
            Console.WriteLine("Письмо отправлено");
        }
    }
}

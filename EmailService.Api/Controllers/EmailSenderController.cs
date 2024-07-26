using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmailService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly ILogger<EmailSenderController> _logger;
        private readonly IEmailService _emailService;

        public EmailSenderController(IEmailService emailService,ILogger<EmailSenderController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        // POST api/<EmailSenderController>
        [HttpPost("send")]
        public async Task PostAsync([FromBody] EmailServiceMessage message)
        {
            try
            {
                await _emailService.SendEmailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в Post");
                throw;
            }
        }

    }
}

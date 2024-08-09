using Microsoft.AspNetCore.Mvc;

namespace EmailServices.Api.Controllers
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

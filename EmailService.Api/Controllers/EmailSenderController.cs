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
        // POST api/<EmailSenderController>
        [HttpPost("send")]
        public void Post([FromBody] EmailServiceMessage message)
        {
            var _email = new EmailService();
            _email.SendEmailAsync(message).GetAwaiter();
        }

    }
}

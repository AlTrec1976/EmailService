using EmailService.Entity;
using Microsoft.Extensions.Options;

namespace EmailServices.Api.Services
{
    public class EmailWorker : BaseKafkaWorker<EmailServiceMessage>
    {
        private readonly ILogger<EmailWorker> _logger;
        private readonly IOptions<EmailGroup> _options;
        private readonly IOptions<SmtpConnect> _smtpoptions;
        private readonly IConfiguration _configuration;

        public EmailWorker(ILogger<EmailWorker> logger, IOptions<EmailGroup> options, IOptions<SmtpConnect> options1, IConfiguration configuration)
        : base(logger)
        {
            _logger = logger;
            _options = options;
            _smtpoptions = options1;
            _configuration = configuration;
        }

        protected override IDictionary<string, string> GetConfiguration()
        {
            var options = _configuration.GetSection(nameof(EmailGroup)).Get<EmailGroup>();
            return new Dictionary<string, string>
            {
                { "KafkaServer", _configuration.GetConnectionString("Kafka") },
                { "GroupId", _options.Value.Group },
                { "Topic", _options.Value.Topic },
            };
        }

        protected override async Task ProccesMessange(EmailServiceMessage msg)
        {
            try
            {
                var loggerEmail = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<EmailSendService>();
                var options = _configuration.GetSection(nameof(SmtpConnect)).Get<SmtpConnect>();

                var emailService = new EmailSendService(loggerEmail, _smtpoptions);

                _logger.LogInformation($"{DateTime.Now} Сообщение для {msg.EmailTo} отправлено в рассылку");

                await emailService.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} Ошибка отправки сообщения в рассылку для {msg.EmailTo}");
                throw;
            }
        }
    }
}

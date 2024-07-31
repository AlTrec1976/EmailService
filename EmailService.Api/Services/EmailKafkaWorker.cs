namespace EmailService.Api.Services
{
    public class EmailKafkaWorker : BaseKafkaWorker<EmailServiceMessage>
    {
        private readonly ILogger<EmailKafkaWorker> _logger;
        private readonly IConfiguration _configuration;

        public EmailKafkaWorker(ILogger<EmailKafkaWorker> logger, IConfiguration configuration)
        : base(logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override string GetTopicName()
        {
            return "email.service.topic";
        }

        protected override async Task ProccesMessange(EmailServiceMessage msg)
        {
            try
            {
                var emailService = new EmailService((ILogger<EmailService>)_logger, _configuration);
                await emailService.SendEmailAsync(msg);

                _logger.LogInformation($"Сообщение для {msg.EmailTo} отправлено в рассылку");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при отправки сообщения для {msg.EmailTo}");
                throw;
            }
        }
    }
}

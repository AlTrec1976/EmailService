namespace EmailServices.Api.Services
{
    public class EmailWorker : BaseKafkaWorker<EmailServiceMessage>
    {
        private readonly ILogger<EmailWorker> _logger;
        private readonly IConfiguration _configuration;

        public EmailWorker(ILogger<EmailWorker> logger, IConfiguration configuration)
        : base(logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override IDictionary<string, string> GetConfiguration()
        {
            return new Dictionary<string, string>
            {
                { "KafkaServer", _configuration.GetConnectionString("Kafka") },
                { "GroupId", _configuration.GetSection("KafkaGroup:EmailGroup:Group").Value },
                { "Topic", _configuration.GetSection("KafkaGroup:EmailGroup:Topic").Value },

            };
        }

        protected override async Task ProccesMessange(EmailServiceMessage msg)
        {
            try
            {
                var loggerEmail = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<EmailService>();
                var emailService = new EmailService(loggerEmail, _configuration);

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

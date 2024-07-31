using Confluent.Kafka;
using System.Text.Json;

namespace EmailService.Api.Services
{
    public abstract class BaseKafkaWorker<T> : BackgroundService
    {
        protected abstract string GetTopicName();
        protected abstract Task ProccesMessange(T msg);

        private readonly ILogger<BaseKafkaWorker<T>> _logger;

        public BaseKafkaWorker(ILogger<BaseKafkaWorker<T>> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async () => await HandleMessageAsync(stoppingToken));
        }

        private async Task HandleMessageAsync(CancellationToken stoppingToken)
        {
            try
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "email_consumer",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(GetTopicName());

                    while (true)
                    {
                        T? consumeResult = JsonSerializer.Deserialize<T>(consumer.Consume(stoppingToken).Message.Value);

                        await ProccesMessange(consumeResult);
                        _logger.LogInformation("Сообщение обработано успешно");
                    }

                    consumer.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке сообщения");
                throw;
            }
        }
    }
}

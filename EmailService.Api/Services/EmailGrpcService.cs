using EmailService.Entity;
using EmailServiceGrpcApp;
using EmailServices;
using EmailServices.Api.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EmailService.Api.Services
{
    public class EmailGrpcService : EmailServiceGrpc.EmailServiceGrpcBase
    {
        private readonly ILogger<EmailGrpcService> _logger;
        private readonly IOptions<SmtpConnect> _smtpoptions;
        private readonly IConfiguration _configuration;

        public EmailGrpcService(ILogger<EmailGrpcService> logger, IOptions<SmtpConnect> options, IConfiguration configuration)
        {
            _logger = logger;
            _smtpoptions = options;
            _configuration = configuration;
        }

        public override async Task<EmailReply> Send(EmailRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC is working");
            var msg = new EmailServiceMessage
            {
                EmailTo = request.EmailTo,
                EmailFrom = request.EmailFrom,
                MessageBody = request.MessageBody
            };

            var loggerEmail = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<EmailSendService>();
            var options = _configuration.GetSection(nameof(SmtpConnect)).Get<SmtpConnect>();

            var emailService = new EmailSendService(loggerEmail, _smtpoptions);

            await emailService.SendEmailAsync(msg);

            var reply = new EmailReply
            {
                EmailTo = request.EmailTo,
                EmailFrom = request.EmailFrom,
                MessageBody = request.MessageBody
            };

            return reply;
        }
    }
}
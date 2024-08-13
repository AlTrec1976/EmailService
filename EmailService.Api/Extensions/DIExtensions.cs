using EmailService.Api.Services;
using EmailServices.Api.Services;

namespace EmailServices.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailSendService>();
            services.AddGrpc();

            //services.AddScoped<EmailGrpc>();
            //services.AddHostedService<EmailGrpc>();
            //services.AddHostedService<EmailWorker>();

            return services;
        }
    }
}
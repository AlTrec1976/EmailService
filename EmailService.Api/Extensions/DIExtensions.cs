using EmailService.Api.Services;

namespace EmailService.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddHostedService<EmailKafkaWorker>();

            return services;
        }
    }
}

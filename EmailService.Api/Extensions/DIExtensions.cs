using EmailServices.Api.Services;

namespace EmailServices.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddHostedService<EmailWorker>();

            return services;
        }
    }
}
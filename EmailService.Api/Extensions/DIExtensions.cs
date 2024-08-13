namespace EmailServices.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailSendService>();
            services.AddGrpc();

            //services.AddHostedService<EmailWorker>();

            return services;
        }
    }
}
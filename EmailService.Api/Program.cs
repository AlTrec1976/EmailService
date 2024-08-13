using EmailService.Api.Services;
using EmailService.Entity;
using EmailServices.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<SmtpConnect>(configuration.GetSection(nameof(SmtpConnect)));
builder.Services.Configure<EmailGroup>(configuration.GetSection(nameof(EmailGroup)));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<EmailGrpcService>(); 
app.MapControllers();

app.Run();

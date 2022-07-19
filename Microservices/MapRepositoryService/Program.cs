using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Infrastructure.IocContainer;
using MessageBroker.Core.Configuration;
using MessageBroker.Infrastructure.IocContainer;
using Minio;
using Serilog;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = builder.Configuration.GetSection(("Settings")).Get<Settings>();
var msgBrokerSettings = builder.Configuration.GetSection(("MessageBroker")).Get<MessageBrokerSettings>();

builder.Services.AddMapRepositoryServices(settings);
builder.Services.AddRabbitMqInfrastructureLayer(msgBrokerSettings);

#region Serlog

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
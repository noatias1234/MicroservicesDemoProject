using MessageBroker.Core.Configuration;
using MessageBroker.Infrastructure.IocContainer;
using Microsoft.AspNetCore.Http.Connections;
using NotificationsService;
using NotificationsService.Commands;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;
using NotificationsService.Hubs;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var settings = builder.Configuration.GetSection(nameof(Settings)).Get<Settings>();

builder.Services.AddRabbitMqInfrastructureLayer(new MessageBrokerSettings
{
    HostName = settings.HostName
});

builder.Services.AddSingleton(settings);

builder.Services.AddSingleton<INewMapEntityCommand, NewMapEntityCommand>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<NotificationsHub>("/ws", options =>
{
    options.Transports = HttpTransportType.WebSockets;
});

app.Run();
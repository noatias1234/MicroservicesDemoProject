using NotificationsService;
using NotificationsService.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddSignalR();


var app = builder.Build();

app.MapHub<NotificationsHub>("/ws");

app.Run();
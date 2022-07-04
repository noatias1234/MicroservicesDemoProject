using MessageBroker.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;
using NotificationsService.Hubs;

namespace NotificationsService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISubscriber _subscriber;
    private readonly IHubContext<NotificationsHub> _hubContext;

    public Worker(ILogger<Worker> logger, ISubscriber subscriber, 
        IHubContext<NotificationsHub> hubContext)
    {
        _logger = logger;
        _subscriber = subscriber;
        _hubContext = hubContext;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _hubContext.Clients.All.SendAsync("message", stoppingToken);
    }
}
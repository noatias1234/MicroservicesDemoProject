using MessageBroker.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;
using NotificationsService.Hubs;

namespace NotificationsService.Commands;

public class NewMapEntityCommand : INewMapEntityCommand
{
    private readonly ILogger<NewMapEntityCommand> _logger;
    private readonly IHubContext<NotificationsHub> _hubContext;
    private readonly ISubscriber _subscriber;
    private readonly Settings _settings;

    public NewMapEntityCommand(ILogger<NewMapEntityCommand> logger, IHubContext<NotificationsHub> hubContext,
        ISubscriber subscriber, Settings settings)
    {
        _logger = logger;
        _hubContext = hubContext;
        _subscriber = subscriber;
        _settings = settings;
    }

    public void NotifyClientsNewMapEntity(string message)
    {
        var clientMethod = _settings.GetNewMapEntityClientMethod;
        if (string.IsNullOrEmpty(clientMethod))
        {
            _logger.LogWarning("ClientMethod: {ClientMethod} was not found", clientMethod);
            return;
        }

        _hubContext.Clients.All.SendAsync( message);
    }
}
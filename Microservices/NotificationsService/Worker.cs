using MessageBroker.Core.Interfaces;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;

namespace NotificationsService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISubscriber _subscriber;
    private readonly INewMapEntityCommand _newMapEntityCommand;
    private readonly Settings _settings;

    public Worker(ILogger<Worker> logger, ISubscriber subscriber, 
        INewMapEntityCommand newMapEntityCommand, Settings settings)
    {
        _logger = logger;
        _subscriber = subscriber;
        _newMapEntityCommand = newMapEntityCommand;
        _settings = settings;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var topic = _settings.MapEntityTopic;

        if (string.IsNullOrEmpty(topic))
        {
            _logger.LogWarning("Topic: {Topic} was not found", topic);
        }
        else _subscriber.Subscribe(topic, _newMapEntityCommand.NotifyClientsNewMapEntity);
        
        return Task.CompletedTask;
    }
}
using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapEntitiesService.Infrastructure;

public class Publisher : IPublisher
{
    private readonly ILogger<Publisher> _logger;

    public Publisher(ILogger<Publisher> logger)
    {
        _logger = logger;
    }

    public void Publish(string topic, MapEntityDto mapEntityDto)
    {
        _logger.LogInformation("Published New Map Entity!!!");
    }
}
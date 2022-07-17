using System.Text.Json;
using MapEntitiesService.Core.Configuration;
using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using MapEntitiesService.Core.Validation.Interfaces;
using MessageBroker.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapEntitiesService.Core.Services;

public class MapEntityService : IMapEntityService
{
    private readonly ILogger<MapEntityService> _logger;
    private readonly IPublisher _publisher;
    private readonly IMapEntityValidator _mapEntityValidator;
    private readonly Settings _settings;

    public MapEntityService(
        ILogger<MapEntityService> logger,
        IPublisher publisher,
        IMapEntityValidator mapEntityValidator, Settings settings)
    {
        _logger = logger;
        _publisher = publisher;
        _mapEntityValidator = mapEntityValidator;
        _settings = settings;
    }
    public ResultModel HandleNewMapEntity(MapEntityDto mapEntityDto)
    {
        _logger.LogInformation("New map entity: mapEntityDto - {mapEntityDto}", mapEntityDto);

        var validationResult = _mapEntityValidator.Validate(mapEntityDto);

        if (!validationResult.Success)
        {
            return new ResultModel(Success: false);
        }

        var messageJson = JsonSerializer.Serialize(mapEntityDto); // Convert to Json
       
        _publisher.Publish(_settings.MapEntityTopic, messageJson);  
        
        return new ResultModel(Success: true);
    }
}
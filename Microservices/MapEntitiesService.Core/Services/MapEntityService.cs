using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using MapEntitiesService.Core.Validation.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapEntitiesService.Core.Services;

public class MapEntityService : IMapEntityService
{
    private readonly ILogger<MapEntityService> _logger;
    private readonly IPublisher _publisher;
    private readonly IMapEntityValidator _mapEntityValidator;

    public MapEntityService(
        ILogger<MapEntityService> logger,
        IPublisher publisher,
        IMapEntityValidator mapEntityValidator)
    {
        _logger = logger;
        _publisher = publisher;
        _mapEntityValidator = mapEntityValidator;
    }
    public ResultModel HandleNewMapEntity(MapEntityDto mapEntityDto)
    {
        _logger.LogInformation("New map entity: Title  - {entityTitle}", mapEntityDto.Title);

        var validationResult = _mapEntityValidator.Validate(mapEntityDto);

        if (validationResult.Success is true)
        {
            _publisher.Publish("MapEntityTopic", mapEntityDto);
            return new ResultModel(Success: true);
        }
        else return new ResultModel(Success: false);
    }
}
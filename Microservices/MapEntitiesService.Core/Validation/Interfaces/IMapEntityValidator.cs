using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Validation.Interfaces;

public interface IMapEntityValidator
{
    ResultModel ValidateMapEntity(MapEntityDto mapEntityDto);
}

using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Validation.Interfaces;

public interface IMapEntityTitleValidator
{
    ResultModel Validate(MapEntityDto mapEntityDto );
}
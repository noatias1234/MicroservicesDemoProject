using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Validation.Validator.Interfaces;

public interface IMapEntityTitleValidator
{
    ResultModel Validate(MapEntityDto mapEntityDto );
}
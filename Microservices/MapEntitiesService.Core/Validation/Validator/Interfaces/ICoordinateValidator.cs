using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Validation.Validator.Interfaces;

public interface ICoordinateValidator
{
    ResultModel Validate(MapEntityDto mapEntityDto);
}
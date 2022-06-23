using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Validation;

public interface ICoordinateValidator
{
    ResultModel ValidateCoordinate(MapEntityDto mapEntityDto);
}

using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Validation.Interfaces;

namespace MapEntitiesService.Core.Validation;

public class MapEntityValidator : IMapEntityValidator
{
    private readonly IMapEntityTitleValidator _mapEntityTitleValidator;
    private readonly ICoordinateValidator _coordinateValidator;

    public MapEntityValidator(IMapEntityTitleValidator mapEntityTitleValidator, ICoordinateValidator coordinateValidator)
    {
        _mapEntityTitleValidator = mapEntityTitleValidator;
        _coordinateValidator = coordinateValidator;
    }
    public ResultModel ValidateMapEntity(MapEntityDto mapEntityDto)
    {
      _mapEntityTitleValidator.ValidateMapEntityTitle(mapEntityDto);
       _coordinateValidator.ValidateCoordinate(mapEntityDto);
       
        return new ResultModel(Success: true);
    }
}

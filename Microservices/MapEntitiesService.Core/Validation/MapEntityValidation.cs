using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Validation.Interfaces;
using MapEntitiesService.Core.Validation.Validator.Interfaces;

namespace MapEntitiesService.Core.Validation;

public class MapEntityValidation : IMapEntityValidator
{
    private readonly IMapEntityTitleValidator _mapEntityTitleValidator;
    private readonly ICoordinateValidator _coordinateValidator;

    public MapEntityValidation(IMapEntityTitleValidator mapEntityTitleValidator, ICoordinateValidator coordinateValidator)
    {
        _mapEntityTitleValidator = mapEntityTitleValidator;
        _coordinateValidator = coordinateValidator;
    }
    public ResultModel Validate(MapEntityDto mapEntityDto)
    {
        var result = _mapEntityTitleValidator.Validate(mapEntityDto);
        if (result.Success is false) return new ResultModel(Success: false);

        result = _coordinateValidator.Validate(mapEntityDto);
        if (result.Success is false) return new ResultModel(Success: false);

        return new ResultModel(Success: true);
    }
}
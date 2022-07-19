using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Validation.Validator.Interfaces;

namespace MapEntitiesService.Core.Validation.Validator;

public class CoordinateValidator : ICoordinateValidator
{
    public ResultModel Validate(MapEntityDto mapEntityDto)
    {
        if (mapEntityDto.XPosition is null)
            return new ResultModel(Success: false, ErrorMessage: "X Position is not valid");

        if (mapEntityDto.YPosition is null)
            return new ResultModel(Success: false, ErrorMessage: "Y Position is not valid");

        return new ResultModel(Success: true);
    }
}
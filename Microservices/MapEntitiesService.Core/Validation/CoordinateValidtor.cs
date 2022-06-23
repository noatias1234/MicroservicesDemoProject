using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Validation;

public class CoordinateValidtor : ICoordinateValidator
{
    public ResultModel ValidateCoordinate(MapEntityDto mapEntityDto)
    {
        if (mapEntityDto.XPosition is null)
            return new ResultModel(Success: false, ErrorMessage: "X Position is not valid");

        if (mapEntityDto.YPosition is null)
            return new ResultModel(Success: false, ErrorMessage: "Y Position is not valid");

        return new ResultModel(Success: true);
    }
}

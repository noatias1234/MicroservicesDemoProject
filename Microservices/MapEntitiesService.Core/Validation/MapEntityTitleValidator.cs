using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Validation.Interfaces;

namespace MapEntitiesService.Core.Validation;

public class MapEntityTitleValidator : IMapEntityTitleValidator
{
    public ResultModel ValidateMapEntityTitle(MapEntityDto mapEntityDto)
    {
        if (string.IsNullOrEmpty(mapEntityDto.Title))
            return new ResultModel(Success: false, ErrorMessage: "Title not valid");

        return new ResultModel(Success: true);
    }
}

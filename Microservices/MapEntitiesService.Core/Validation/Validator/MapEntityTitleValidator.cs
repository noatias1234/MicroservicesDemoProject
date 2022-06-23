using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Validation.Interfaces;

namespace MapEntitiesService.Core.Validation.Validator;

public class MapEntityTitleValidator : IMapEntityTitleValidator
{
    public ResultModel Validate(MapEntityDto mapEntityDto)
    {
        if (string.IsNullOrEmpty(mapEntityDto.Title))
            return new ResultModel(Success: false, ErrorMessage: "Title not valid");

        else return new ResultModel(Success: true);
    }
}
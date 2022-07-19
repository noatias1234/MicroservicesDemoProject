using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Validation.Interface;
public interface IUploadMapValidation
{
    ResultModel Validate(MapModelDto mapModelDto);
}

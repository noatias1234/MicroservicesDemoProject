using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Validation.Validators.Interfaces;

public interface IMapTypeValidator
{
    ResultModel MapFileExtensionsValidate(string? extension);

}
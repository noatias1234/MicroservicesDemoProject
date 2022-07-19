using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Validation.Validators.Interfaces;
public interface IMapNameValidator
{
    ResultModel MapNameExistValidate(string? mapName);
    ResultModel MapNameValidate(string? mapName);
    ResultModel Validate(string? mapName);

}

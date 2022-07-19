using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Validation.Validators.Interfaces;
public interface IMapFileValidator
{
    ResultModel MapSizeValidate(Stream? fileStream);
    ResultModel MapDataValidate(Stream fileStream);
    ResultModel Validate (Stream? fileStream);
}

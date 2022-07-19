using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapRepositoryService.Core.Validation.Validators;
public class MapTypeValidator : IMapTypeValidator
{
    public ResultModel MapFileExtensionsValidate(string? extension)
    {
        var supportedTypes = new[] { "png", "jpg", "svg" ,"jpeg"};

        return supportedTypes.Contains(extension) 
            ? new ResultModel(true) 
            : new ResultModel(false, "Invalid extension");
    }
}
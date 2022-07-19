using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Validation.Interface;
using MapRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapRepositoryService.Core.Validation;
public class UploadMapValidation : IUploadMapValidation
{
    private readonly IMapFileValidator _mapFileValidator;
    private readonly IMapNameValidator _mapNameValidator;
    private readonly IMapTypeValidator _mapTypeValidator;

    public UploadMapValidation(IMapFileValidator mapFileValidator,
        IMapNameValidator mapNameValidator, IMapTypeValidator mapTypeValidator)
    {
        _mapFileValidator = mapFileValidator;
        _mapNameValidator = mapNameValidator;
        _mapTypeValidator = mapTypeValidator;
    }

    public ResultModel Validate(MapModelDto mapModelDto)
    {
        if (_mapFileValidator.Validate(mapModelDto.MapFile).Success.Equals(false))
            return new ResultModel(false, "Invalid map file");
        return _mapNameValidator.Validate(mapModelDto.MapName).Success.Equals(false)
            ? new ResultModel(false, "Invalid map name")
            : _mapTypeValidator.MapFileExtensionsValidate(mapModelDto.Extension);
    }
}

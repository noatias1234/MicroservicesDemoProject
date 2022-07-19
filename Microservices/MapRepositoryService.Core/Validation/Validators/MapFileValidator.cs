using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapRepositoryService.Core.Validation.Validators;

public class MapFileValidator : IMapFileValidator
{
    public ResultModel MapSizeValidate(Stream? fileStream)
    {
        if (fileStream == null) return new ResultModel(false);
        var fileSize = fileStream.Length;

        return fileSize > 1048576
            ? new ResultModel(false, "Invalid file size")
            : new ResultModel(true);
    }

    public ResultModel MapDataValidate(Stream? fileStream)
    {
        return fileStream is null 
            ? new ResultModel(false, "Data is null") 
            : new ResultModel(true);
    }

    public ResultModel Validate(Stream? fileStream)
    {
        return MapSizeValidate(fileStream).Success.Equals(false) 
            ? new ResultModel(false, "MapSizeValidate is false") 
            : MapDataValidate(fileStream);
    }
}
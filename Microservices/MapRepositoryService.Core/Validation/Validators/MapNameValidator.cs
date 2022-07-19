using System.Text.RegularExpressions;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapRepositoryService.Core.Validation.Validators;
public class MapNameValidator : IMapNameValidator
{
    public ResultModel MapNameExistValidate(string? mapName)
    {
        return new ResultModel(true);
    }

    public ResultModel MapNameValidate(string? mapName)
    {
        if (string.IsNullOrEmpty(mapName))
        {
            return new ResultModel(false, "Map name is empty");
        }

        return Regex.Match(mapName, "[a-zA-Z][0-9]$").Success 
            ? new ResultModel(true) 
            : new ResultModel(false, "Invalid map name");
    }

    public ResultModel Validate(string? mapName)
    {
        return MapNameExistValidate(mapName).Success.Equals(false)
            ? new ResultModel(false, "MapName is already exist")
            : MapNameValidate(mapName);
    }
}

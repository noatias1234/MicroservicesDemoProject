using MapEntitiesService.Core.Model;
namespace MapEntitiesService.Core.Validation.Interfaces;
public interface IMapEntityValidator
{
    ResultModel Validate(MapEntityDto mapEntityDto);
}
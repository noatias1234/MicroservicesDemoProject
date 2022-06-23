using MapEntitiesService.Core.Model;
namespace MapEntitiesService.Core.Services.Interfaces;

public interface IMapEntityService
{ 
    ResultModel HandleNewMapEntity(MapEntityDto mapEntityDto);
}

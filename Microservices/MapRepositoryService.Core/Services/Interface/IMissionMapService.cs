using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Services.Interface;
public interface IMissionMapService
{
    Task<MapResultModel> GetMissionMap();
    void SetMissionMap(string mapName);
}

using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Data.Mission.Interfaces.Query;
public interface IGetMissionMap
{
    Task<MapResultModel> Get();
}

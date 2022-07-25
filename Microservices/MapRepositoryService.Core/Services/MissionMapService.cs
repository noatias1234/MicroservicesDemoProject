using MapRepositoryService.Core.Data.Mission.Interfaces.Command;
using MapRepositoryService.Core.Data.Mission.Interfaces.Query;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Services.Interface;
using Microsoft.Extensions.Logging;

namespace MapRepositoryService.Core.Services;
public class MissionMapService : IMissionMapService
{
    private readonly ILogger<MissionMapService> _logger;
 
    private readonly IGetMissionMap _getMissionMapQuery;
    private readonly ISetMissionMapCommand _setMissionMap;

    public MissionMapService(ILogger<MissionMapService> logger, IGetMissionMap getMissionMapQuery
        , ISetMissionMapCommand setMissionMap)
    {
        _logger = logger;
        _getMissionMapQuery = getMissionMapQuery;
        _setMissionMap = setMissionMap;
    }
    public async Task<MapResultModel> GetMissionMap()
    {
        var missionMap = await _getMissionMapQuery.Get();
        _logger.LogInformation(" GetMissionMap: This is the mission map : {mapBase64}", missionMap);
        return missionMap;
    }

    public void SetMissionMap(string mapName)
    {
        _setMissionMap.SetMap(mapName);
        _logger.LogInformation(" SetMissionMap: This is the mission map : {mapName}", mapName);

    }
}

using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MapRepositoryService.Controllers;
[Route("[controller]")]
[ApiController]
public class MissionMapController : ControllerBase
{
    private readonly IMissionMapService _missionMapService;

    public MissionMapController(IMissionMapService missionMapService)
    {
        _missionMapService = missionMapService;
    }

    [HttpGet]
    public Task<MapResultModel> GetMissionMapRequest()
    {
        return _missionMapService.GetMissionMap();
    }

    [HttpPost]
    public void SetMapMission([FromForm] string mapName)
    {
        _missionMapService.SetMissionMap(mapName);
    }
}
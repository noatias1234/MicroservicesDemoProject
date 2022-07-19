using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MapRepositoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsRepositoryController : ControllerBase
    {
        private readonly IMapRepositoryService _mapRepositoryService;

        public MapsRepositoryController(IMapRepositoryService mapRepositoryService)
        {
            _mapRepositoryService = mapRepositoryService;
        }

        [HttpGet]
        public List<string> GetAllMaps()
        {
            return _mapRepositoryService.GetAllMaps();
        }

        [HttpGet("{mapName}")]
        public Stream? GetMapByName(string mapName)
        {
            return _mapRepositoryService.GetMapByName(mapName);
        }

        [HttpDelete]
        public void DeleteMap(string mapName)
        {
            _mapRepositoryService.DeleteMapByMapName(mapName);
        }

        [HttpPost]
        public ResultModel PostMap([FromForm] UploadMapDto mapDto)
        {
            var memoryStream = new MemoryStream();
            mapDto.MapFile?.CopyTo(memoryStream);

            var mapModel = new MapModelDto()
            {
                MapName = mapDto.MapName,
                MapFile = memoryStream,
                Extension = Path.GetExtension(mapDto.MapFile?.FileName)
            };
            
           return _mapRepositoryService.HandleMapRepository(mapModel);
        }
    }


    // 1. Get all maps - return list of strings
    // 2. Upload new map ( need validations ) 
    // 3. Delete map by name - selected
    // 4. Get map by name

    // 5. Mission map controller - 
    // Set mission map
    // Get mission map
}
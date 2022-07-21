using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MapRepositoryService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MapsRepositoryController : ControllerBase
    {
        private readonly IMapRepositoryService _mapRepositoryService;
        public record UploadMapDto(string MapName, IFormFile MapFile);

        public MapsRepositoryController(IMapRepositoryService mapRepositoryService)
        {
            _mapRepositoryService = mapRepositoryService;
        }

        [HttpGet]
        public Task<List<string>> GetAllMaps()
        {
             return _mapRepositoryService.GetAllMaps();
        }

        [HttpGet("{mapName}")]
        public void GetMapByName(string mapName)
        {
             _mapRepositoryService.GetMapByName(mapName);
        }

        [HttpDelete]
        public void DeleteMap(string mapName)
        {
            _mapRepositoryService.DeleteMapByMapName(mapName);
        }

        [HttpPost]
        public ResultModel PostMapAsync([FromForm] UploadMapDto mapDto)
        {
            var mapModel = new MapModelDto
            {
                MapName = mapDto.MapName,
                MapFile = mapDto.MapFile.OpenReadStream(),
                Extension = Path.GetExtension(mapDto.MapFile.FileName)
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
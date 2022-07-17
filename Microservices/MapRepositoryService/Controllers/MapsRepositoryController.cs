using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MapRepositoryService.Controller
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

        [HttpGet ("{mapName}")]
        public Stream GetMapByName(Stream mapName)
        {
            return _mapRepositoryService.GetMapByName(mapName);
        }
      
        [HttpDelete]
        public void DeleteMap(Stream mapName)
        {
            _mapRepositoryService.DeleteMapByMapName(mapName);
        }
    }


    // 1. Get all maps - return list of strings
    // 2. Upload new map ( need validations ) 
    // 3. Delete map by name - selected
    // 4. Get map by name

    //5. Mission map controller - 
        // Set mission map
        // Get mission map
}
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

        #region Maps

        [HttpGet]
        public Task<List<string>> Get()
        { 
            return _mapRepositoryService.GetAllMaps();
        }

        [HttpGet("{mapName}")]
        public void Get(string mapName)
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

            return _mapRepositoryService.HandleMap(mapModel);
        }
        #endregion

    }
}

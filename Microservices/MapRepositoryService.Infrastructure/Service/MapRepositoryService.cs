using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Services.Interface;
using MapRepositoryService.Core.Validation.Interface;
using MessageBroker.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapRepositoryService.Infrastructure.Service
{
    public class MapRepositoryService : IMapRepositoryService
    {
        private readonly ILogger<MapRepositoryService> _logger;
        private readonly Settings _settings;
        private readonly IPublisher _publisher;
        private readonly IUploadMapValidation _mapValidation;

        public MapRepositoryService(ILogger<MapRepositoryService> logger, Settings settings, IPublisher publisher, IUploadMapValidation mapValidation)
        {
            _logger = logger;
            _settings = settings;
            _publisher = publisher;
            _mapValidation = mapValidation;
        }

        public void DeleteMapByMapName(string mapName)
        {
           _logger.LogInformation(("Delete map: mapName - {mapName}", mapName).ToString());
        }

        public List<string> GetAllMaps()
        {
            //var mapFile = Convert.ToBase64String(memoryStream.ToArray());
            throw new NotImplementedException();
        }
        public Stream? GetMapByName(string mapName)
        {
            _logger.LogInformation(("Get map: fileName - {fileName}", mapName).ToString());
            //var mapFile = Convert.ToBase64String(memoryStream.ToArray());

            Stream? mapFile= null;
            return mapFile;

        }
        public ResultModel HandleMapRepository(MapModelDto mapDto)
        {
            _logger.LogInformation("mapFile : {mapFile} , mapName: {mapName}", mapDto.MapFile, mapDto.MapName);
            _publisher.Publish(_settings.MapRepositoryTopic, mapDto.ToString());
            return _mapValidation.Validate(mapDto) ;
        }
    }
}

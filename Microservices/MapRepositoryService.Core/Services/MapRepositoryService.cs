using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Commands.Interfaces;
using MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Core.Services.Interface;
using MapRepositoryService.Core.Validation.Interface;
using MessageBroker.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapRepositoryService.Core.Services
{
    public class MapRepositoryService : IMapRepositoryService
    {
        private readonly ILogger<MapRepositoryService> _logger;
        private readonly Settings _settings;
        private readonly IPublisher _publisher;
        private readonly IUploadMapValidation _mapValidation;
        private readonly IDeleteMapCommand _deleteMap;
        private readonly IGetAllMapsQuery _getAllMaps;
        private readonly IGetMapByName _getMapByName;
        private readonly IUpdateMapCommand _updateMap;

        public MapRepositoryService(ILogger<MapRepositoryService> logger, 
            Settings settings, IPublisher publisher, IUploadMapValidation mapValidation,
            IDeleteMapCommand deleteMap, IGetAllMapsQuery getAllMaps, IGetMapByName getMapByName, IUpdateMapCommand updateMap)
        {
            _logger = logger;
            _settings = settings;
            _publisher = publisher;
            _mapValidation = mapValidation;
            _deleteMap = deleteMap;
            _getAllMaps = getAllMaps;
            _getMapByName = getMapByName;
            _updateMap = updateMap;
        }

        public void DeleteMapByMapName(string mapName)
        {
           _logger.LogInformation(("Delete map: mapName - {mapName}", mapName).ToString());
           _deleteMap.Delete(mapName);
        }
        public Task<List<string>> GetAllMaps()
        {
            return _getAllMaps.Get(_settings.MapBucketName);
        }
        public void GetMapByName(string mapName)
        {
            _logger.LogInformation(("Get map: fileName - {fileName}", mapName).ToString());

             _getMapByName.Get(_settings.MapBucketName, mapName);
            
        }
        public ResultModel HandleMapRepository(MapModelDto mapDto)
        {
            _logger.LogInformation("mapFile : {mapFile} , mapName: {mapName}", mapDto.MapFile, mapDto.MapName);
            _publisher.Publish(_settings.MapRepositoryTopic, mapDto.ToString());
            if (_mapValidation.Validate(mapDto).Success)
            {
                _updateMap.Update(mapDto);
            }

            return _mapValidation.Validate(mapDto) ;
        }
    }
}

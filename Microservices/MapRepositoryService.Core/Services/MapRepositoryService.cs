using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Services.Interface;
using MessageBroker.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapRepositoryService.Core.Services
{
    internal class MapRepositoryService : IMapRepositoryService
    {
        private readonly ILogger<MapRepositoryService> _logger;
        private readonly IPublisher _publisher;
        private readonly Settings _settings;

        public MapRepositoryService(ILogger<MapRepositoryService> logger,
            IPublisher publisher, Settings settings)
        {
            _logger = logger;
            _publisher = publisher;
            _settings = settings;
        }

        public void DeleteMapByMapName(Stream fileName)
        {
           _logger.LogInformation(("Delete map: fileName - {fileName}", fileName).ToString());
        }

        public List<string> GetAllMaps()
        {
            throw new NotImplementedException();
        }

        public Stream GetMapByName(Stream mapName)
        {
            _logger.LogInformation(("Get map: fileName - {fileName}", mapName).ToString());
            return mapName;

        }
    }
}

using System.Reactive.Linq;
using System.Text.Json;
using MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;

namespace MapRepositoryService.Infrastructure.Data.Maps.Queries;

internal class GetAllMapsQuery : IGetAllMapsQuery
{
    private readonly ILogger<GetAllMapsQuery> _logger;
    private readonly IMinIoClientBuilder _minIoClientBuilder;

    public GetAllMapsQuery(ILogger<GetAllMapsQuery> logger,
        IMinIoClientBuilder minIoClientBuilder)
    {
        _logger = logger;
        _minIoClientBuilder = minIoClientBuilder;
    }

    public async Task<List<string>> Get(string bucketName)
    {
        var minio = _minIoClientBuilder.Build(bucketName);
        try
        {
            var allObjects = await minio.ListObjectsAsync(bucketName).ToList();
            var mapsList = allObjects.Select(map => map.Key).ToList();

            var allMaps = JsonSerializer.Serialize(allObjects);
            _logger.LogInformation("GetAllMaps succeeded {allMaps} ", allMaps);

            return mapsList;
        }
        catch (Exception)
        {
            _logger.LogInformation("GetAllMaps failed");
            throw new Exception("GetAllMaps failed");
        }
    }
}
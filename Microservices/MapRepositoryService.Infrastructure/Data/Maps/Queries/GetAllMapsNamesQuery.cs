using System.Reactive.Linq;
using System.Text.Json;
using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
using MapRepositoryService.Infrastructure.Minio;
using MessageBroker.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Queries;

internal class GetAllMapsNamesQuery : IGetAllMapsQuery
{
    private readonly ILogger<GetAllMapsNamesQuery> _logger;
    private readonly MinioClient _minioClient;
    private readonly IPublisher _publisher;

    public GetAllMapsNamesQuery(ILogger<GetAllMapsNamesQuery> logger,
        IMinIoClientBuilder minioClientBuilder, IPublisher publisher , Settings settings)
    {
        _logger = logger;
        _publisher = publisher;
        _minioClient = minioClientBuilder.Build(settings.MapBucketName);
    }

    public async Task<List<string>> Get(string bucketName)
    {
        try
        {
            var memoryStream = new MemoryStream();

            var listArgs = new ListObjectsArgs().WithBucket(bucketName);
            var files = await _minioClient.ListObjectsAsync(listArgs).ToList();
            var mapList = files.Select(f => f.Key).ToList();

            _logger.LogInformation("GetAllMaps succeeded {allMaps} ", mapList);
            return mapList;
        }
        catch (Exception)
        {
            _logger.LogInformation("GetAllMaps failed");
            throw new Exception("GetAllMaps failed");
        }
    }
}
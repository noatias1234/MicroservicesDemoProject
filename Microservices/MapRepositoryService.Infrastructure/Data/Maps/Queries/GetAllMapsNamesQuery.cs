using System.Reactive.Linq;
using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Interfaces.Queries;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Queries;

internal class GetAllMapsNamesQuery : IGetAllMapsQuery
{
    private readonly ILogger<GetAllMapsNamesQuery> _logger;
    private readonly MinioClient _minioClient;

    public GetAllMapsNamesQuery(ILogger<GetAllMapsNamesQuery> logger,
        IMinIoClientBuilder minioClientBuilder, Settings settings)
    {
        _logger = logger;
        _minioClient = minioClientBuilder.Build(settings.MapBucketName);
    }

    public async Task<List<string>> Get(string bucketName)
    {
        try
        {
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
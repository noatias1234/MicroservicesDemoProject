using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Queries;
public class GetMapByName : IGetMapByName
{
    private readonly ILogger<GetMapByName> _logger;
    private readonly Settings _settings;
    private readonly MinioClient _minioClient;

    public GetMapByName(ILogger<GetMapByName> logger, Settings settings ,IMinIoClientBuilder minioClientBuilder)
    {
        _logger = logger;
        _settings = settings;
        _minioClient = minioClientBuilder.Build(_settings.MapBucketName);
    }
    public async Task<string> Get(string bucketName, string mapName)
    {
        try
        {
            var memoryStream = new MemoryStream();
            
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(mapName).WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                });
            
            await _minioClient.GetObjectAsync(args);

            _logger.LogInformation("GetMapByName succeeded {memoryStream} ", memoryStream);
            
            return Convert.ToBase64String(memoryStream.ToArray());
        }
        catch (Exception)
        {
            _logger.LogInformation("GetMapByName failed");
            throw new Exception("GetMapByName failed");
        }
    }
}

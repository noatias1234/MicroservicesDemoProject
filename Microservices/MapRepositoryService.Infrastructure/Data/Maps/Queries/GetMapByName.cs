using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Interfaces.Queries;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Queries;
public class GetMapByName : IGetMapByName
{
    private readonly ILogger<GetMapByName> _logger;
    private readonly MinioClient _minioClient;

    public GetMapByName(ILogger<GetMapByName> logger, Settings settings, IMinIoClientBuilder minioClientBuilder)
    {
        _logger = logger;
        _minioClient = minioClientBuilder.Build(settings.MapBucketName);
    }
    public async Task<MapResultModel> Get(string bucketName, string mapName)
    {
        MapResultModel result;
        try
        {
            var bytes = Array.Empty<byte>();

            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(mapName).WithCallbackStream(stream =>
                {
                    using var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                });

            var stat = await _minioClient.GetObjectAsync(args);
            var extension = Path.GetExtension(stat.ObjectName).Replace(".", string.Empty);
            
            result = new MapResultModel()
            {
                MetaData = $"data:image/{extension};base64",
                ImageBase64 = Convert.ToBase64String(bytes)
            };

            _logger.LogInformation("GetMapByName succeeded {result} ", result);

        }
        catch (Exception)
        {
            _logger.LogInformation("GetMapByName failed");
            throw new Exception("GetMapByName failed");
        }

        return result;
    }
}
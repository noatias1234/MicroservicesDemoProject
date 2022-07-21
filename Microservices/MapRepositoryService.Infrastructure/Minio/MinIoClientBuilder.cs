using MapRepositoryService.Core.Configuration;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Minio;
public class MinIoClientBuilder : IMinIoClientBuilder
{
    private readonly ILogger<MinIoClientBuilder> _logger;
    private readonly Settings _setting;

    public MinIoClientBuilder(ILogger<MinIoClientBuilder> logger, Settings setting)
    {
        _logger = logger;
        _setting = setting;
    }

    public MinioClient Build(string bucketName)
    {
        try
        {
            var minIoClient = new MinioClient()
                .WithEndpoint(_setting.EndPoint)
                .WithCredentials(_setting.AccessKey, _setting.SecretKey)
                .Build();

            if (minIoClient is null)
                throw new Exception();

            CreateBucket(minIoClient, bucketName).ConfigureAwait(false).GetAwaiter().GetResult();

            return minIoClient;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task CreateBucket(MinioClient minioClient, string mapBucketName)
    {
        try
        {
            var args = new BucketExistsArgs()
                .WithBucket(mapBucketName);
            var found = await minioClient.BucketExistsAsync(args);
           
            if (!found)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(mapBucketName);
                await minioClient.MakeBucketAsync(makeBucketArgs);
            }

        }
        catch (Exception x)
        {
            _logger.LogWarning("CreateMapBucket is failed");
            throw new Exception("CreateMapBucket is failed");
        }
    }
}

using MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Queries;
public class GetMapByName : IGetMapByName
{
    private readonly ILogger<GetMapByName> _logger;
    private readonly IMinIoClientBuilder _minIoClient;

    public GetMapByName(ILogger<GetMapByName> logger, IMinIoClientBuilder minIoClient)
    {
        _logger = logger;
        _minIoClient = minIoClient;
    }
    public async Task<Stream> Get(string bucketName, string mapName)
    {
        var minio = _minIoClient.Build(bucketName);
        try
        {
            var memoryStream = new MemoryStream();
            
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(mapName).WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                });
            
            await minio.GetObjectAsync(args);
           
            _logger.LogInformation("GetMapByName succeeded {memoryStream} ", memoryStream);
            
            return memoryStream;
        }
        catch (Exception)
        {
            _logger.LogInformation("GetMapByName failed");
            throw new Exception("GetMapByName failed");
        }
    }
}

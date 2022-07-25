using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Interfaces.Commands;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Commands;
internal class DeleteMapCommand : IDeleteMapCommand
{
    private readonly ILogger<DeleteMapCommand> _iLogger;
    private readonly MinioClient _minioClient;
    private readonly Settings _settings;


    public DeleteMapCommand(ILogger<DeleteMapCommand> iLogger,
        IMinIoClientBuilder minioClientBuilder, Settings settings)
    {
        _iLogger = iLogger;
        _settings = settings;
        _minioClient = minioClientBuilder.Build(_settings.MapBucketName);
    }

    public async Task Delete(string mapName)
    { 
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(_settings.MapBucketName)
                .WithObject(mapName);

            await _minioClient.RemoveObjectAsync(args);
            _iLogger.LogInformation("{mapName} removed", mapName);
        }
        catch (Exception)
        {
            _iLogger.LogWarning("Cannot delete {mapName}", mapName);
            throw new Exception("Cannot delete map");

        }
    }
}

using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Commands.Interfaces;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Commands;
internal class DeleteMapCommand : IDeleteMapCommand
{
    private readonly ILogger<DeleteMapCommand> _iLogger;
    private readonly IMinIoClientBuilder _minioClient;
    private readonly Settings _settings;


    public DeleteMapCommand(ILogger<DeleteMapCommand> iLogger,
        IMinIoClientBuilder minioClient, Settings settings)
    {
        _iLogger = iLogger;
        _minioClient = minioClient;
        _settings = settings;
    }
    public async Task Delete(string mapName)
    { 
        var minio = _minioClient.Build(_settings.MapBucketName);
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(_settings.MapBucketName)
                .WithObject(mapName);

            await minio.RemoveObjectAsync(args);
            _iLogger.LogInformation("{mapName} removed", mapName);
        }
        catch (Exception)
        {
            _iLogger.LogWarning("Cannot delete {mapName}", mapName);
            throw new Exception("Cannot delete map");

        }
    }
}

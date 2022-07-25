using System.ComponentModel.Design;
using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Interfaces.Commands;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Infrastructure.Minio;
using MessageBroker.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Commands;
internal class CreateMapCommand : ICreateMapCommand
{
    private readonly ILogger<DeleteMapCommand> _iLogger;
    private readonly IPublisher _publisher;
    private readonly Settings _settings;
    private readonly MinioClient _minioClient;

    public CreateMapCommand(ILogger<DeleteMapCommand> iLogger,
        IMinIoClientBuilder minIoClientBuilder, IPublisher publisher, Settings settings)
    {
        _iLogger = iLogger;
        _publisher = publisher;
        _settings = settings;
        _minioClient = minIoClientBuilder.Build("maps");
    }

    public async Task Create(MapModelDto mapDto)
    {
        try
        {
            var args = new PutObjectArgs()
                .WithBucket("maps")
                .WithObject(mapDto.MapName)
                .WithStreamData(mapDto.MapFile)
                .WithObjectSize(mapDto.MapFile!.Length)
                .WithContentType("application/octet-stream");
            await _minioClient.PutObjectAsync(args);

            _publisher.Publish(_settings.MapRepositoryTopic, mapDto.MapName);
            _iLogger.LogInformation("Uploaded object {MapName} to maps bucket", mapDto.MapName);
        }
        catch (Exception e)
        {
            _iLogger.LogWarning("[Bucket]  Exception: {e}", e);
        }
    }
}
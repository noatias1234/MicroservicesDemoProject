using MapRepositoryService.Core.Data.Maps.Commands.Interfaces;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Commands;
internal class CreateMapCommand : ICreateMapCommand
{
    private readonly ILogger<DeleteMapCommand> _iLogger;
    private readonly MinioClient _minioClient;

    public CreateMapCommand(ILogger<DeleteMapCommand> iLogger,
        IMinIoClientBuilder minIoClientBuilder)
    {
        _iLogger = iLogger;
        _minioClient = minIoClientBuilder.Build("maps");
    }

    public async Task Create(MapModelDto mapDto)
    {
        try
        {
            if (mapDto.MapFile != null)
            {
                var args = new PutObjectArgs()
                    .WithBucket("maps")
                    .WithObject(mapDto.MapName)
                    .WithStreamData(mapDto.MapFile)
                    .WithObjectSize(mapDto.MapFile.Length)
                    .WithContentType("application/octet-stream");
                await _minioClient.PutObjectAsync(args);
            }

            _iLogger.LogInformation("Uploaded object {MapName} to maps bucket",mapDto.MapName);
        }
        catch (Exception e)
        {
          _iLogger.LogWarning("[Bucket]  Exception: {e}", e);
        }
    }
}
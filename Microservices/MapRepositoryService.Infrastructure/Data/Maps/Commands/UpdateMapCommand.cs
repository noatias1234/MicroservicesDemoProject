using MapRepositoryService.Core.Data.Maps.Commands.Interfaces;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Maps.Commands;
internal class UpdateMapCommand : IUpdateMapCommand
{
    private readonly ILogger<DeleteMapCommand> _iLogger;
    private readonly IMinIoClientBuilder _minioClient;

    public UpdateMapCommand(ILogger<DeleteMapCommand> iLogger,
        IMinIoClientBuilder minioClient)
    {
        _iLogger = iLogger;
        _minioClient = minioClient;
    }
    public async Task Update(MapModelDto mapDto)
    {
        var minio = _minioClient.Build("maps");
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
                await minio.PutObjectAsync(args);
            }

            _iLogger.LogInformation("Uploaded object {MapName} to maps bucket",mapDto.MapName);
        }
        catch (Exception e)
        {
          _iLogger.LogWarning("[Bucket]  Exception: {e}", e);
        }
    }
}
using MapRepositoryService.Core.Configuration;
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
    private readonly Settings _settings;

    public UpdateMapCommand(ILogger<DeleteMapCommand> iLogger,
        IMinIoClientBuilder minioClient, Settings settings)
    {
        _iLogger = iLogger;
        _minioClient = minioClient;
        _settings = settings;
    }
    public async Task Update(MapModelDto mapDto)
    {
        var minio = _minioClient.Build("maps");
        try
        {
            var args = new PutObjectArgs()
                    .WithBucket("maps")
                    .WithObject(mapDto.MapName)
                    .WithStreamData(mapDto.MapFile)
                    .WithObjectSize(mapDto.MapFile.Length)
                    .WithContentType("application/octet-stream");
                await minio.PutObjectAsync(args);
            

            Console.WriteLine($"Uploaded object {mapDto.MapName} to maps bucket");
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}

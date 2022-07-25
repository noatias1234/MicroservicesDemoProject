using System.Reactive.Linq;
using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Mission.Interfaces.Command;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Mission.Command;
internal class SetMissionMapCommand : ISetMissionMapCommand
{
    private readonly ILogger<SetMissionMapCommand> _logger;
    private readonly Settings _settings;
    private readonly MinioClient _minioClient;

    public SetMissionMapCommand(ILogger<SetMissionMapCommand> logger, Settings settings, IMinIoClientBuilder minIoClientBuilder)
    {
        _logger = logger;
        _settings = settings;
        _minioClient = minIoClientBuilder.Build("mission");
    }
    public async void SetMap(string mapName)
    {
        try
        {
            await RemoveMissionMapsList();
            await CopyMapToMission(mapName);
            
            _logger.LogInformation("{mapName} is the mission map" , mapName);
        }
        catch (Exception e)
        {
            _logger.LogWarning("[Bucket]  Exception: {e}", e);
        }
    }
    private async Task CopyMapToMission(string mapName)
    {
        var cpSrcArgs = new CopySourceObjectArgs() //Copy from source
            .WithBucket(_settings.MapBucketName)
            .WithObject(mapName);

        var copyArgs = new CopyObjectArgs() //To target
            .WithBucket(_settings.MissionMapBucketName)
            .WithObject(mapName)
            .WithCopyObjectSource(cpSrcArgs);

        await _minioClient.CopyObjectAsync(copyArgs);
    }

    private async Task RemoveMissionMapsList()
    {
        var listArgs = new ListObjectsArgs().WithBucket(_settings.MissionMapBucketName);
        var missionMapsList = await _minioClient.ListObjectsAsync(listArgs).ToList();

        foreach (var missionMap in missionMapsList)
        {
            var args = new RemoveObjectArgs()
                .WithBucket(_settings.MissionMapBucketName)
                .WithObject(missionMap.Key);
            await _minioClient.RemoveObjectAsync(args);
        }
    }
}
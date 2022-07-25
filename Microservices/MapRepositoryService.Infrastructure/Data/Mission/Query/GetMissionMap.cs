using System.Reactive.Linq;
using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Mission.Interfaces.Query;
using MapRepositoryService.Core.Model;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapRepositoryService.Infrastructure.Data.Mission.Query;
internal class GetMissionMap : IGetMissionMap
{
    private readonly ILogger<GetMissionMap> _logger;
    private readonly Settings _settings;
    private readonly MinioClient _minioClient;
    public GetMissionMap(ILogger<GetMissionMap> logger,Settings settings, IMinIoClientBuilder minIoClientBuilder)
    {
        _logger = logger;
        _settings = settings;
        _minioClient = minIoClientBuilder.Build("mission");
    }
    public async Task<MapResultModel> Get()
    {
        try
        {
            var ms = new MemoryStream();
            var listArgs = new ListObjectsArgs().WithBucket(_settings.MissionMapBucketName);
            var missionMapList = await _minioClient.ListObjectsAsync(listArgs);

            var args = new GetObjectArgs()
                .WithBucket(_settings.MissionMapBucketName)
                .WithObject(missionMapList.Key).WithCallbackStream(stream => stream.CopyTo(ms));
        
            var stat = await _minioClient.GetObjectAsync(args);
            var extension = Path.GetExtension(stat.ObjectName).Replace(".", string.Empty);

            var result = new MapResultModel()
            {
                MetaData = $"data:image/{extension};base64",
                ImageBase64 = Convert.ToBase64String(ms.ToArray())
            };

            _logger.LogInformation("This is the mission map {missionMap} ", stat.ObjectName);
            return result;
        }
        catch (Exception e)
        {
            switch (e)
            {
                case null:
                {
                      _logger.LogWarning("Mission bucket is null");
                        return new MapResultModel
                        {
                            MetaData = string.Empty,
                            ImageBase64 = string.Empty
                        };
                }
                default:
                    Console.WriteLine(e);
                    throw;
            }
        }
    }
}

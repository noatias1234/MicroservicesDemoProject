using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Data.Maps.Interfaces.Queries;
public interface IGetMapByName
{
    public Task<MapResultModel> Get(string bucketName, string mapName);
}

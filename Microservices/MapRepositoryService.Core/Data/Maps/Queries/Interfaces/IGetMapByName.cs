using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
public interface IGetMapByName
{
    public Task<string> Get(string bucketName, string mapName);
}

namespace MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
public interface IGetAllMapsQuery
{
    public Task<List<string>> Get(string bucketName);
}

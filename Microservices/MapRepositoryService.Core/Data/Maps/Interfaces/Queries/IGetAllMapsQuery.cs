namespace MapRepositoryService.Core.Data.Maps.Interfaces.Queries;
public interface IGetAllMapsQuery
{
    public Task<List<string>> Get(string bucketName);
}

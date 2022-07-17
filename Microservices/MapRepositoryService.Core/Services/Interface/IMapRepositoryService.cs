namespace MapRepositoryService.Core.Services.Interface
{
    public interface IMapRepositoryService
    {
        void DeleteMapByMapName(Stream fileName);

        List<string> GetAllMaps();

        Stream GetMapByName(Stream mapName);

        
    }
}

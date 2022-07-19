using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Services.Interface
{
    public interface IMapRepositoryService
    {
        void DeleteMapByMapName(string mapName);

        List<string> GetAllMaps();

        Stream? GetMapByName(string mapName);

        ResultModel HandleMapRepository(MapModelDto mapDto);
    }
}

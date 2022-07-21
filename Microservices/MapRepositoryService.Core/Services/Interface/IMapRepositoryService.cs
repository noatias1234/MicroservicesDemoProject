using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Services.Interface
{
    public interface IMapRepositoryService
    {
        void DeleteMapByMapName(string mapName);

       Task<List<string>> GetAllMaps();

        void GetMapByName(string mapName);

        ResultModel HandleMapRepository(MapModelDto mapDto);
    }
}

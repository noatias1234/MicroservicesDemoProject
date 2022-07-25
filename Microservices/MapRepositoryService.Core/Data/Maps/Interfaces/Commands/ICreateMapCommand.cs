using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Data.Maps.Interfaces.Commands;
public interface ICreateMapCommand
{
    Task Create(MapModelDto mapDto);
}

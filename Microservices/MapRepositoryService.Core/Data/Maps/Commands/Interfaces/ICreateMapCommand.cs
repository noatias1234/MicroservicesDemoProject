using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Data.Maps.Commands.Interfaces;
public interface ICreateMapCommand
{
    Task Create(MapModelDto mapDto);
}

using MapRepositoryService.Core.Model;

namespace MapRepositoryService.Core.Data.Maps.Commands.Interfaces;
public interface IUpdateMapCommand
{
    Task Update(MapModelDto mapDto);
}

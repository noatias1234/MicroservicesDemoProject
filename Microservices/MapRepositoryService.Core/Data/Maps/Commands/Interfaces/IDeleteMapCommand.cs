namespace MapRepositoryService.Core.Data.Maps.Commands.Interfaces;

public interface IDeleteMapCommand
{
    Task Delete(string mapName);
}

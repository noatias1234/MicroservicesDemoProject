namespace MapRepositoryService.Core.Data.Maps.Interfaces.Commands;

public interface IDeleteMapCommand
{
    Task Delete(string mapName);
}

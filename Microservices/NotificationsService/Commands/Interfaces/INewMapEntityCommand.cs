namespace NotificationsService.Commands.Interfaces;

public interface INewMapEntityCommand
{ 
    void NotifyClientsNewMapEntity(string message);
}
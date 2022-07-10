namespace MapPresenterApp.Core.Interface;
public interface ISignalRService
{
    Task StartAsync();
    Task DisconnectAsync();
    void GetNewMapPoint(Action<string> callBack);

}

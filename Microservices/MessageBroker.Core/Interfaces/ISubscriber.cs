namespace MessageBroker.Core.Interfaces;
public interface ISubscriber
{
    void ReceiveMessage(string? topic, Action<string>? callbackFunction);
}

namespace MessageBroker.Core.Interfaces;
public interface ISubscriber
{
    void Subscribe(string? topic, Action<string>? callbackFunction);
}

namespace MessageBroker.Core.Interfaces;
public interface IPublisher
{
    void Publish(string topic, string? message); 
}

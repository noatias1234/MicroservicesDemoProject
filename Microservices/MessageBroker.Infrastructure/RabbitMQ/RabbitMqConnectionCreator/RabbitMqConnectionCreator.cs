using MessageBroker.Core.Configuration;
using MessageBroker.Infrastructure.RabbitMQ.RabbitMqConnectionCreator.Interfaces;
using RabbitMQ.Client;

namespace MessageBroker.Infrastructure.RabbitMQ.RabbitMqConnectionCreator;
internal class RabbitMqConnectionCreator : IRabbitMqConnectionCreator
{
    private readonly ConnectionFactory _factory;
    public RabbitMqConnectionCreator(MessageBrokerSettings messageBrokerSettings)
    {
        _factory = new ConnectionFactory() { HostName = messageBrokerSettings.HostName };
    }

    public IModel Create()
    {
        var connection = _factory.CreateConnection();
        return connection.CreateModel();
    }
}
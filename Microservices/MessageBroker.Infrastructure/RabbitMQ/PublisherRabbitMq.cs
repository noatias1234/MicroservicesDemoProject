using System.Text;
using MessageBroker.Core.Interfaces;
using MessageBroker.Infrastructure.RabbitMQ.RabbitMqConnectionCreator.Interfaces;
using RabbitMQ.Client;

namespace MessageBroker.Infrastructure.RabbitMQ;

internal class PublisherRabbitMq : IPublisher, IDisposable
{
 
    private readonly IModel _channel;

    public PublisherRabbitMq(IRabbitMqConnectionCreator connection)
    {
        _channel = connection.Create();
    }

    public void Dispose()
    {
        _channel.Dispose();
    }

    public void Publish(string topic, string? message)
    {
        _channel.ExchangeDeclare(exchange: topic, type: ExchangeType.Fanout);

        if (message == null)
        {
            return;
        }

        var body = Encoding.UTF8.GetBytes(message); 

        _channel.BasicPublish(exchange: topic,
            routingKey: "",
            basicProperties: null,
            body: body);
    }
}
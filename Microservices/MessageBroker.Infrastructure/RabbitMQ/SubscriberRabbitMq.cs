using System.Text;
using MessageBroker.Core.Interfaces;
using MessageBroker.Infrastructure.RabbitMQ.RabbitMqConnectionCreator.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBroker.Infrastructure.RabbitMQ;
internal class SubscriberRabbitMq : ISubscriber, IDisposable
{
    private readonly IModel _channel;

    public SubscriberRabbitMq(IRabbitMqConnectionCreator connectionCreator)
    {
        _channel = connectionCreator.Create();
    }

    public void Dispose()
    {
        _channel.Dispose();
    }

    public void ReceiveMessage(string? topic, Action<string>? callbackFunction)
    {
        _channel.ExchangeDeclare(exchange: topic, type: ExchangeType.Fanout);

        var queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(
            queue: queueName,
            exchange: topic,
            routingKey: "");


        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            callbackFunction?.Invoke(message);
        };

        _channel.BasicConsume(queue: queueName,
            autoAck: true,
            consumer: consumer);
    }
}
using RabbitMQ.Client;

namespace MessageBroker.Infrastructure.RabbitMQ.RabbitMqConnectionCreator.Interfaces;
internal interface IRabbitMqConnectionCreator
{ 
    IModel Create();
}

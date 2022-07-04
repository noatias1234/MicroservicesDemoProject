using MessageBroker.Core.Configuration;
using MessageBroker.Core.Interfaces;
using MessageBroker.Infrastructure.RabbitMQ;
using MessageBroker.Infrastructure.RabbitMQ.RabbitMqConnectionCreator;
using MessageBroker.Infrastructure.RabbitMQ.RabbitMqConnectionCreator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker.Infrastructure.IocContainer;
public static class ServicesCollectionExtension
{
    public static void AddRabbitMqInfrastructureLayer(this IServiceCollection services, MessageBrokerSettings messageBrokerSettings)
    {
        services.AddSingleton(messageBrokerSettings);
        services.AddSingleton<IRabbitMqConnectionCreator, RabbitMqConnectionCreator>();
        services.AddSingleton<IPublisher, PublisherRabbitMq>();
        services.AddSingleton<ISubscriber, SubscriberRabbitMq>();
    }
}

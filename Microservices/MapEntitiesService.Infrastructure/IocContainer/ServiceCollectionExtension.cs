using MapEntitiesService.Core.Configuration;
using MapEntitiesService.Core.Services;
using MapEntitiesService.Core.Services.Interfaces;
using MapEntitiesService.Core.Validation;
using MapEntitiesService.Core.Validation.Interfaces;
using MapEntitiesService.Core.Validation.Validator;
using MapEntitiesService.Core.Validation.Validator.Interfaces;
using MessageBroker.Core.Configuration;
using MessageBroker.Infrastructure.IocContainer;
using Microsoft.Extensions.DependencyInjection;

namespace MapEntitiesService.Infrastructure.IocContainer;
public static class ServiceCollectionExtension
{
    public static void AddMapEntityServices(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton(settings);

        services.AddRabbitMqInfrastructureLayer(new MessageBrokerSettings
        {
            HostName = settings.HostName
        });

        services.AddScoped<IMapEntityService, MapEntityService>();
        services.AddScoped<IMapEntityValidator, MapEntityValidation>();
        services.AddScoped<IMapEntityTitleValidator, MapEntityTitleValidator>();
        services.AddScoped<ICoordinateValidator, CoordinateValidtor>();

    }
}
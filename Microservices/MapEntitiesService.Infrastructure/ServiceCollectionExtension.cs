using MapEntitiesService.Core.Services;
using MapEntitiesService.Core.Services.Interfaces;
using MapEntitiesService.Core.Validation;
using MapEntitiesService.Core.Validation.Interfaces;
using MapEntitiesService.Core.Validation.Validator;
using MapEntitiesService.Core.Validation.Validator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MapEntitiesService.Infrastructure;
public static class ServiceCollectionExtension
{
    public static void AddMapEntityServices(this IServiceCollection services)
    {
        services.AddScoped<IPublisher, Publisher>();
        services.AddScoped<IMapEntityService, MapEntityService>();
        services.AddScoped<IMapEntityValidator, MapEntityValidator>();
        services.AddScoped<IMapEntityTitleValidator, MapEntityTitleValidator>();
        services.AddScoped<ICoordinateValidator, CoordinateValidtor>();
    }
}
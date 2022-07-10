using MapPresenterApp.Core.Configuration;
using MapPresenterApp.Core.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace MapPresenterApp.Infrastructure.IocContainer;

public static class ServicesCollectionsContainer
{
    public static void AddMapPresenterInfrastructureLayers(this IServiceCollection services, Settings settings)
    {
        services.AddScoped<ISignalRService, SignalRService.SignalRService>();
        services.AddSingleton(settings);
    }
}

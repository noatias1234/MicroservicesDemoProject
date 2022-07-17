using MapRepositoryService.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MapRepositoryService.Infrastructure.IocContainer
{
    public static class ServiceCollectionExtension 
    {
        public static void AddMapRepositoryServices(this IServiceCollection services, Settings settings)
        {
            services.AddSingleton(settings);
        }

    }
}

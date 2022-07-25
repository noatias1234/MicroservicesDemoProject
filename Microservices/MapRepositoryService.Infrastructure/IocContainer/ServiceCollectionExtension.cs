using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Interfaces.Commands;
using MapRepositoryService.Core.Data.Maps.Interfaces.Queries;
using MapRepositoryService.Core.Data.Mission.Interfaces.Command;
using MapRepositoryService.Core.Data.Mission.Interfaces.Query;
using MapRepositoryService.Core.Services;
using MapRepositoryService.Core.Services.Interface;
using MapRepositoryService.Core.Validation;
using MapRepositoryService.Core.Validation.Interface;
using MapRepositoryService.Core.Validation.Validators;
using MapRepositoryService.Core.Validation.Validators.Interfaces;
using MapRepositoryService.Infrastructure.Data.Maps.Commands;
using MapRepositoryService.Infrastructure.Data.Maps.Queries;
using MapRepositoryService.Infrastructure.Data.Mission.Command;
using MapRepositoryService.Infrastructure.Data.Mission.Query;
using MapRepositoryService.Infrastructure.Minio;
using Microsoft.Extensions.DependencyInjection;

namespace MapRepositoryService.Infrastructure.IocContainer
{
    public static class ServiceCollectionExtension
    {
        public static void AddMapRepositoryServices(this IServiceCollection services, Settings settings)
        {
            services.AddSingleton(settings);
            services.AddScoped<IMapFileValidator, MapFileValidator>();
            services.AddScoped<IMapNameValidator, MapNameValidator>();
            services.AddScoped<IMapTypeValidator, MapTypeValidator>();
            services.AddScoped<IUploadMapValidation, UploadMapValidation>();
            services.AddScoped<IDeleteMapCommand, DeleteMapCommand>();
            services.AddScoped<ICreateMapCommand,CreateMapCommand>();
            services.AddScoped<IGetAllMapsQuery, GetAllMapsNamesQuery>();
            services.AddScoped<IGetMapByName, GetMapByName>();
            services.AddScoped<IMinIoClientBuilder, MinIoClientBuilder>();
            services.AddScoped<IGetMissionMap, GetMissionMap>();
            services.AddScoped<ISetMissionMapCommand, SetMissionMapCommand>();
            services.AddScoped<IMissionMapService, MissionMapService>();
            services.AddScoped<IMapRepositoryService, Core.Services.MapRepositoryService>();
        }
    }
}
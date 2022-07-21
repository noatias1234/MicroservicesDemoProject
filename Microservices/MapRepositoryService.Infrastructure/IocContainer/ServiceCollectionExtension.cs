using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Data.Maps.Commands.Interfaces;
using MapRepositoryService.Core.Data.Maps.Queries.Interfaces;
using MapRepositoryService.Core.Services.Interface;
using MapRepositoryService.Core.Validation;
using MapRepositoryService.Core.Validation.Interface;
using MapRepositoryService.Core.Validation.Validators;
using MapRepositoryService.Core.Validation.Validators.Interfaces;
using MapRepositoryService.Infrastructure.Data.Maps.Commands;
using MapRepositoryService.Infrastructure.Data.Maps.Queries;
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
            services.AddScoped<IUpdateMapCommand,UpdateMapCommand>();
            services.AddScoped<IGetAllMapsQuery, GetAllMapsQuery>();
            services.AddScoped<IGetMapByName, GetMapByName>();
            services.AddScoped<IMinIoClientBuilder, MinIoClientBuilder>();
            services.AddScoped<IMapRepositoryService, Core.Services.MapRepositoryService>();

        }
    }
}
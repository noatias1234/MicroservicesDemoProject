using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Core.Services.Interface;
using MapRepositoryService.Core.Validation;
using MapRepositoryService.Core.Validation.Interface;
using MapRepositoryService.Core.Validation.Validators;
using MapRepositoryService.Core.Validation.Validators.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Minio;
using Minio.AspNetCore;

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
            services.AddScoped<IMapRepositoryService, Service.MapRepositoryService>();
        }
    }
}
using System.Windows;
using MapPresenterApp.Core.Configuration;
using MapPresenterApp.Infrastructure.IocContainer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MapPresenterApp;

public partial class App : Application
{
    private readonly IHost _host;
    public App()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var settings = config.Get<Settings>();

        _host = Host.CreateDefaultBuilder()
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddMapPresenterInfrastructureLayers(settings);
                services.AddScoped<MainWindow>();
            })
            .Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }
    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }
        base.OnExit(e);
    }
}
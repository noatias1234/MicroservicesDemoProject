using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((_, config) =>
{
    config.AddJsonFile("ocelot.json");
});

builder.Services.AddSignalR();
builder.Services.AddOcelot();

#region Serilog

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("All",
        policy =>
        {
            policy.
                AllowAnyOrigin().AllowAnyHeader().
                AllowAnyMethod();
        });
});
#endregion

var app = builder.Build();

app.UseCors();
app.UseWebSockets();
await app.UseOcelot();

app.Run();

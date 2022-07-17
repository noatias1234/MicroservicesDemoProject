using MapRepositoryService.Core.Configuration;
using MapRepositoryService.Infrastructure.IocContainer;
using Minio;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = builder.Configuration.GetSection(("Settings")).Get<Settings>();
builder.Services.AddMapRepositoryServices(settings);

#region Serlog

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region minio

MinioClient minioClient = new MinioClient()
    .WithEndpoint("play.min.io")
    .WithCredentials("Q3AM3UQ867SPQQA43P2F", "zuf+tfteSlswRu7BJ86wekitnifILbZam1KYY3TG")
    .WithSSL()
    .Build();

#endregion
app.UseAuthorization();

app.MapControllers();

app.Run();
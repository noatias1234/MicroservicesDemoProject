using MapEntitiesService.Core.Configuration;
using MapEntitiesService.Infrastructure.IocContainer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 var settings = builder.Configuration.GetSection(("MessageBrokerSettings")).Get<Settings>();
builder.Services.AddMapEntityServices(settings);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

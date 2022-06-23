using MapEntitiesService.Core.Services;
using MapEntitiesService.Core.Services.Interfaces;
using MapEntitiesService.Core.Validation;
using MapEntitiesService.Core.Validation.Interfaces;
using MapEntitiesService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddScoped<IPublisher, Publisher>();
builder.Services.AddScoped<IMapEntityService, MapEntityService>();
builder.Services.AddScoped<IMapEntityValidator, MapEntityValidator>();
builder.Services.AddScoped<IMapEntityTitleValidator, MapEntityTitleValidator>();
builder.Services.AddScoped<ICoordinateValidator, CoordinateValidtor>();


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

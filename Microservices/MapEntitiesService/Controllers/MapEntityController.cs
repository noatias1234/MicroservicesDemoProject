using Microsoft.AspNetCore.Mvc;

namespace MapEntitiesService.Controllers;

[ApiController]
[Route("[controller]")]
public class MapEntityController : ControllerBase
{
    private readonly ILogger<MapEntityController> _logger;

    public record ResultModel (bool Success, string ErrorMessage = "");

    public MapEntityController(ILogger<MapEntityController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public ResultModel Post(
        [FromForm]MapEntityDto mapEntityDto, 
        [FromServices] IPublisher publisher)
    {
        _logger.LogInformation("New map entity: Title  - {entityTitle}", mapEntityDto.Title);

        // validate mapEntityDto
        // if validation is failed, log 
        if (string.IsNullOrEmpty(mapEntityDto.Title))
        {
            return new ResultModel(Success: false, ErrorMessage: "Title not valid");
        }

        // publish to message broker by topic name NewMapEntity
        publisher.Publish(topic: "NewMapEntity", mapEntityDto);

        return new ResultModel(Success: true);
    }
}

public interface IPublisher
{
    void Publish(string topic, MapEntityDto mapEntityDto);
}

public class Publisher : IPublisher
{
    private readonly ILogger<Publisher> _logger;

    public Publisher(ILogger<Publisher> logger)
    {
        _logger = logger;
    }

    public void Publish(string topic, MapEntityDto mapEntityDto)
    {
        _logger.LogInformation("Published New Map Entity!!!");
    }
}

public class MapEntityDto
{
    public string? Title { get; set; }
    public double? XPosition { get; set; }
    public double? YPosition { get; set; }
}

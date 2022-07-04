using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapEntitiesService.Controllers;

[ApiController]
[Route("[controller]")]
public partial class MapEntityController : ControllerBase
{
    private readonly IMapEntityService _mapEntityService;

    public MapEntityController(IMapEntityService mapEntityService)
    {
        _mapEntityService = mapEntityService;
    }

    [HttpPost]
    public ResultModel Post([FromBody] MapEntityDto mapEntityDto) => _mapEntityService.HandleNewMapEntity(mapEntityDto);
}
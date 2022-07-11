using System.Text.Json;

namespace MapEntitiesService.Core.Model;

public class MapEntityDto
{
    public string? Title { get; set; }
    public double? XPosition { get; set; }
    public double? YPosition { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}

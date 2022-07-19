using System.Text.Json;

namespace MapRepositoryService.Core.Model;
public class MapModelDto
{
    public string? MapName { get; set; } = "";
    public string? Extension { get; set; } = "";
    public Stream? MapFile { get; set; }


    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

using MessageBroker.Core.Configuration;

namespace NotificationsService.Configurations;

public class Settings
{
    public string? MapEntityTopic { get; set; }

    public string? GetNewMapEntityClientMethod { get; set; }

    public string? HostName { get; set; } 
}

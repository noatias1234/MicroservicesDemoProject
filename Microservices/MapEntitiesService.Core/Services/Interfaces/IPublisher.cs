using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Services.Interfaces;

public interface IPublisher
{
    void Publish(string topic, MapEntityDto mapEntityDto);
}

using Microservices.BuildingBlocks.EventBus.Events;
using Microservices.Foundation.ContentTypes.Enums;
using Microservices.Foundation.ContentTypes.Items;

namespace Microservices.BuildingBlocks.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent integrationEvent);
    Task PublishNotificationAsync(string methodName, Apps appName, BaseItem data, EventResult result, string message, string DeviceGroup=null, string BusinessGroup=null);
}

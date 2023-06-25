using Microservices.BuildingBlocks.EventBus.Events;

namespace Microservices.BuildingBlocks.EventBus.IntegrationEvents.AaryaIntegerationEvent
{
    public record DemoRequestedIntegrationEvent : IntegrationEvent
    {
        public string Message { get; set; }
    }
}

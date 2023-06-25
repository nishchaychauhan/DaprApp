namespace Microservices.BuildingBlocks.EventBus.Events;

public record IntegrationEvent
{
    public Guid Id { get; }
    public DateTime CreationDate { get; }

    public string DuplicateMarker { get; set; }

    public bool RaiseEvent { get; set; } = false;

    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }
}

namespace Eshop.Shared.Messaging.Events;

public class IntegrationEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}

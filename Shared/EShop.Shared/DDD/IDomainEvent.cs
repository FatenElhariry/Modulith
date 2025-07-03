

namespace EShop.Shared.DDD
{
    public interface IDomainEvent : INotification
    {
        public Guid EventId => Guid.NewGuid();

        public DateTime Occurredon { get; set; }
        public string? EventType => GetType().AssemblyQualifiedName;

    }
}

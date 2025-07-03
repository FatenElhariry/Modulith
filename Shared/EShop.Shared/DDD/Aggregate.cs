using EShop.Shared.Domain;
using System.ComponentModel.DataAnnotations.Schema;


namespace EShop.Shared.DDD
{
    public abstract class Aggregate<IId> : Entity<IId>, IAggregate<IId>
        where IId : notnull
    {
        // private readonly List<IDomainEvent> _domainEvents = new();

        [NotMapped]
        public  List<IDomainEvent> DomainEvents { get ; set; } = new List<IDomainEvent>();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));
            DomainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            var events = DomainEvents.ToArray();
            DomainEvents.Clear();
           return events;
        }
    }
}

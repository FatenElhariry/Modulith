
using EShop.Shared.DDD;

namespace EShop.Ordering.Features.Events;

public class OrderCreatedEvent(Order Order) : IDomainEvent
{
    public DateTime Occurredon { get; set ; }
}

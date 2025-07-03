namespace Eshop.Catalog.Products.Events;

public record ProductCreatedEvent(Product product) : IDomainEvent
{
    public DateTime Occurredon { get; set; }
}

namespace Eshop.Catalog.Products.Events;

public record ProductPriceChangeEvent(Product Product) : IDomainEvent
{
    public DateTime Occurredon { get; set; }
}

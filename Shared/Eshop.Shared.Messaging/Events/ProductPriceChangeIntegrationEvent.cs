namespace Eshop.Shared.Messaging.Events;
public class ProductPriceChangeIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; set; } = default;
    public string Name { get; set; }    = default!;
    public decimal Price { get; set; } = default;
    public List<string> Category { get; set; } = default!;
    public string Desciption { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}

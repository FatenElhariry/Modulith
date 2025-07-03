using Eshop.Shared.Messaging.Events;
using MassTransit;

namespace Eshop.Catalog.Products.EventHandlers;

public class ProductPriceChangeEventHandler(IBus bus, ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductPriceChangeEvent>
{
    public async Task Handle(ProductPriceChangeEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event handler {DomainEvent}", notification.GetType().AssemblyQualifiedName);

        var integrationEvent = new ProductPriceChangeIntegrationEvent()
        {
            ProductId = notification.Product.Id,
            Price = notification.Product.Price,
            Name = notification.Product.Name,
            Category = notification.Product.Category,
            Desciption = notification.Product.Description,
            ImageUrl = notification.Product.ImageUrl,

        };

        await bus.Publish(integrationEvent, cancellationToken);
    }
}

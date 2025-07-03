
namespace Eshop.Catalog.Products.EventHandlers;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event handler {DomainEvent}", notification.GetType().AssemblyQualifiedName);
        return Task.CompletedTask;
    }
}

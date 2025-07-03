using EShop.Ordering.Features.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EShop.Ordering.Features.EventHandler;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("");

        return Task.CompletedTask;
    }
}

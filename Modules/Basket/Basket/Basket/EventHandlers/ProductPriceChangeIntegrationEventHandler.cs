using Eshop.Shared.Messaging.Events;
using EShop.Basket.Basket.Features.UpdateBasketItemPrice;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EShop.Basket.Basket.EventHandlers;

public class ProductPriceChangeIntegrationEventHandler(ISender sender,ILogger<ProductPriceChangeIntegrationEventHandler> logger) : IConsumer<Eshop.Shared.Messaging.Events.ProductPriceChangeIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangeIntegrationEvent> context)
    {
        logger.LogInformation("Integration event handler {IntegrationEvent} with ProductId: {ProductId}, Price: {Price}",
            context.Message.GetType().AssemblyQualifiedName, context.Message.ProductId, context.Message.Price);

        var result =  await sender.Send(new UpdateBasketItemPriceCommand(context.Message.ProductId, context.Message.Price));
       
    }
}



using Eshop.Shared.Messaging.Events;
using EShop.Basket.Data.Repository;
using EShop.Shared.Exceptions;
using MassTransit;
using System.Text.Json;

namespace EShop.Basket.Basket.Features.CheckoutBasket;

public record BasketCheckoutCommand (BasketCheckoutDto BasketCheckout) : ICommand<BasketCheckoutResult>;

public record BasketCheckoutResult (bool Success);

public class CheckoutHandler(IBasketRepository repository, IBus bus, BasketDbContext dbContext) : ICommandHandler<BasketCheckoutCommand, BasketCheckoutResult>
{
    public async Task<BasketCheckoutResult> Handle(BasketCheckoutCommand command, CancellationToken cancellationToken)
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var basket = await repository.GetBasket(command.BasketCheckout.UserName, cancellationToken: cancellationToken);

            if (basket != null)
                throw new NotFoundException("Basket", command.BasketCheckout.UserName);

            var eventMessage = command.Adapt<BasketCheckoutIntegrationEvent>();

            eventMessage.TotalPrice = basket!.TotalPrice;

            dbContext.OutboxMessages.Add(new Models.OutboxMessage
            {
                Id = Guid.NewGuid(),
                Content = JsonSerializer.Serialize(basket),
                Type = typeof(BasketCheckoutIntegrationEvent).AssemblyQualifiedName!,
                OccurendOn = DateTime.UtcNow
            });

            await repository.DeleteBasket(command.BasketCheckout.UserName, cancellationToken);
            return new BasketCheckoutResult(true);
        }
        catch 
        {
            await transaction.RollbackAsync();
            return new BasketCheckoutResult(false);
        }
        
    }
}

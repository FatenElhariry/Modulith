

using EShop.Basket.Data;
using EShop.Shared.Exceptions;

namespace EShop.Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool Success);
public class DeleteBasketHandler (BasketDbContext dbContext) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {

        var shoppingCart =  await dbContext.ShoppingCarts.SingleOrDefaultAsync(x => x.UserName == command.UserName);

        if (shoppingCart == null)
            throw new NotFoundException(command.UserName);

        dbContext.ShoppingCarts.Remove(shoppingCart);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteBasketResult(true);
    }
}


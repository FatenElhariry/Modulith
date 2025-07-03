namespace EShop.Basket.Basket.Features.RemoveBasketItem;

public record RemoveBasketItemCommand(string UserName, Guid ProductId) : ICommand<RemoveBasketItemResult>;

public record RemoveBasketItemResult(bool Success);


public class AddBasketItemCommandValidation : AbstractValidator<RemoveBasketItemCommand>
{
    public AddBasketItemCommandValidation()
    {
        RuleFor(command => command.UserName)
           .NotNull()
           .WithMessage("UserName cannot be null.");

        RuleFor(command => command.ProductId)
            .NotEmpty()
            .WithMessage("Product Id is required.");
    }
}


public class RemoveBasketItemHandler(BasketDbContext dbContext) : ICommandHandler<RemoveBasketItemCommand, RemoveBasketItemResult>
{
   
    public async Task<RemoveBasketItemResult> Handle(RemoveBasketItemCommand command, CancellationToken cancellationToken)
    {

        var shoppingCart = await dbContext.
            ShoppingCarts.
            SingleOrDefaultAsync(x => x.UserName == command.UserName);

        if (shoppingCart == null)
            throw new BasketNotFound(command.UserName);

        shoppingCart.RemoveItem(command.ProductId);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RemoveBasketItemResult(true);
    }
}


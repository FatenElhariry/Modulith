
namespace EShop.Basket.Basket.Features.UpdateBasketItemPrice;

public record UpdateBasketItemPriceCommand(Guid ProductId, decimal NewPrice) : ICommand<UpdateBasketItemPriceResponse>;

public record UpdateBasketItemPriceResponse(bool Success, string Message = "");


public class UpdateBasketItemPriceCommandValiation: AbstractValidator<UpdateBasketItemPriceCommand>
{
    public UpdateBasketItemPriceCommandValiation()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.NewPrice).GreaterThan(0).WithMessage("Price must be greater than Zero.");
    }
}

public class UpdateBasketItemPriceHandler(BasketDbContext dbContext) : ICommandHandler<UpdateBasketItemPriceCommand, UpdateBasketItemPriceResponse>
{

    public async Task<UpdateBasketItemPriceResponse> Handle(UpdateBasketItemPriceCommand command, CancellationToken cancellationToken)
    {
        var itemsToupdate = await dbContext.
            ShoppingCartItems.
            Where(x => x.ProductId == command.ProductId).
            ToListAsync(cancellationToken);

        if (!itemsToupdate.Any())
            return new UpdateBasketItemPriceResponse(false, "No related items are defined.");

        foreach (var item in itemsToupdate)
            item.UpdatePrice(command.NewPrice);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateBasketItemPriceResponse(false);
    }
}

using EShop.Basket.Data.Repository;

namespace EShop.Basket.Basket.Features.AddBasketItem;

public record AddBasketItemCommand(string UserName, ShoppingCartItemDto ShoppingCartItem) : ICommand<AddBasketItemResult>;

public record AddBasketItemResult(Guid Id, bool Success);

public class AddBasketItemCommandValidation : AbstractValidator<AddBasketItemCommand>
{
    public AddBasketItemCommandValidation()
    {
        RuleFor(command => command.ShoppingCartItem.ProductName)
           .NotNull()
           .WithMessage("Product Name cannot be null.");

        RuleFor(command => command.ShoppingCartItem.ProductId)
            .NotEmpty()
            .WithMessage("User name is required.");

        RuleFor(command => command.ShoppingCartItem.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}

public class AddBasketItemHandler(IBasketRepository repository, ISender sender) : ICommandHandler<AddBasketItemCommand, AddBasketItemResult>
{

    public async Task<AddBasketItemResult> Handle(AddBasketItemCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

        if (shoppingCart == null)
            throw new BasketNotFound(command.ShoppingCartItem.ShoppingCartId.ToString());

        var productResult = await sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId), cancellationToken);


        shoppingCart.AddItem(command.ShoppingCartItem.ProductId, 
            //command.ShoppingCartItem.ProductName,
            //command.ShoppingCartItem.Price,
            productResult.Product.Name,
            productResult.Product.Price,
            command.ShoppingCartItem.Quantity,
            command.ShoppingCartItem.Color);

        return new AddBasketItemResult(shoppingCart.Id, true);
    }
}


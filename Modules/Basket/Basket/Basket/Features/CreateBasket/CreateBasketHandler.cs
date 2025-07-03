using EShop.Basket.Data.Repository;


namespace EShop.Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<CreateBasketResult>;
public record CreateBasketResult(Guid Id);

public class CreateBasketValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator()
    {
        RuleFor(command => command.ShoppingCart)
            .NotNull()
            .WithMessage("Shopping cart cannot be null.");
        RuleFor(command => command.ShoppingCart.UserName)
            .NotEmpty()
            .WithMessage("User name is required.");
    }
}

public class CreateBasketHandler(IBasketRepository responsitory, ISender sender) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        
        var shoppingCart = CreateNewBasket(command.ShoppingCart, cancellationToken);

        await responsitory.CreateBasket(shoppingCart);
        //dbContext.ShoppingCarts.Add(shoppingCart);
        return new CreateBasketResult(shoppingCart.Id);
    }

    private  ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto, CancellationToken cancellationToken)
    {
        var shoppingCart = ShoppingCart.Create(Guid.NewGuid(), shoppingCartDto.UserName);

        shoppingCartDto.Items.ForEach(async item =>
        {
            var productResult = await sender.Send(new GetProductByIdQuery(item.ProductId), cancellationToken);

            shoppingCart.AddItem(item.ProductId, 
                productResult.Product.Name, 
                productResult.Product.Price, 
                item.Quantity,
                productResult.Product.Name);
        });

        return shoppingCart;
    }

}


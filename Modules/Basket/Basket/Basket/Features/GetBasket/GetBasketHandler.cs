using EShop.Basket.Data.Repository;

namespace EShop.Basket.Basket.Features.GetBasket;
public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCartDto ShoppingCart);
public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(query.UserName, true, cancellationToken);

        if (shoppingCart == null)
            throw new BasketNotFound(query.UserName);

        return new GetBasketResult(shoppingCart.Adapt<ShoppingCartDto>());
    }
}


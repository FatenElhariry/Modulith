
namespace EShop.Basket.Basket.Features.GetBasket;

public record GetBasketResponse(ShoppingCartDto ShoppingCart);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Basket/{UserName}", async (string UserName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(UserName));

            return Results.Ok(new GetBasketResponse(result.ShoppingCart));
        }).Produces<GetBasketResponse>(StatusCodes.Status200OK);
    }
}



namespace EShop.Basket.Basket.Features.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckout);

public record CheckoutBasketResponse(bool Success);

public class CheckoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Basket/Checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<BasketCheckoutCommand>();

            var result = await sender.Send(command);

            return new CheckoutBasketResponse(true);
        });
    }
}




using EShop.Basket.Data.Repository;

namespace EShop.Basket.Basket.Features.CreateBasket
{

    public record CreateBasketRequest(ShoppingCartDto ShoppingCart);
    public record CreateBasketResponse(Guid Id);

    public class CreateBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Basket/{UserName}", async ([FromRoute]string UserName,CreateBasketRequest request, ISender sender) =>
            {
                var result = await sender.Send(new CreateBasketCommand(request.ShoppingCart));

                Results.Created($"/Basket/{UserName}", new CreateBasketResponse(result.Id));
            }).Produces<CreateBasketRequest>(StatusCodes.Status200OK);
        }
    }
}

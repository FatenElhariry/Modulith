

namespace EShop.Basket.Basket.Features.RemoveBasketItem;
public class RemoveBasketItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Basket/{UserName}/Items/{ProductId}", async ([FromRoute]string UserName,[FromRoute]Guid ProductId, ISender sender) =>
        {

            var command = new RemoveBasketItemCommand(UserName, ProductId);
            var result = await sender.Send(command);

            Results.Ok(result);

        }).Produces<RemoveBasketItemResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}


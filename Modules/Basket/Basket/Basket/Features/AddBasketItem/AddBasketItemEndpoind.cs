

namespace EShop.Basket.Basket.Features.AddBasketItem;

public record AddBasketItemRequest(ShoppingCartItemDto ShoppingCartItem);

public record AddBasketItemResponse(bool Success);


public class AddBasketItemEndpoind : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/AddItem/{UserName}/Items", 
            async ([FromRoute]string UserName ,
            [FromBody]AddBasketItemRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new AddBasketItemCommand(UserName, request.ShoppingCartItem));

            return Results.Created($"/basket/{result.Id}", result);
        }).Produces<AddBasketItemResponse>(StatusCodes.Status200OK).
        ProducesProblem(StatusCodes.Status500InternalServerError).
        WithDescription("").
        WithSummary("");
    }
}


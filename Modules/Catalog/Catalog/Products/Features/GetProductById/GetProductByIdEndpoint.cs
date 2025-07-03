using Catalog.Contract.Products.Features.GetProductById;
using Microsoft.AspNetCore.Http;

namespace Eshop.Catalog.Products.Features.GetProductById;


public record GetProductByIdResponse(ProductDto Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Product/{Id}", async (Guid Id, ISender sender) =>
        {
            var result =  await sender.Send(new GetProductByIdQuery(Id));

            return Results.Ok(new GetProductByIdResponse(result.Product));
        }).Produces<GetProductByIdResponse>(StatusCodes.Status200OK).
        ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}


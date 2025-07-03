
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eshop.Catalog.Products.Features.CreateProduct
{
    public record CreateProductRequest(ProductDto Product)
    {

    }

    public record CreateProductResponse(Guid Id)
    {

    }

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created("/Products", response);
            }).WithName("CreateProduct").
            Produces<CreateProductResponse>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).
            WithSummary("Create a new product").
            WithDescription("Create a new product in the catalog.");
        }
    }
}

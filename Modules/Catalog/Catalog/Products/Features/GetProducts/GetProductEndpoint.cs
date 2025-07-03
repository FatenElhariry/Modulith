using Carter;
using EShop.Shared.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Catalog.Products.Features.GetProducts
{
    public record GetProductResponse(PaginatedResult<ProductDto> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery(request));

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            }).WithName("Get Products")
            .Produces<GetProductResponse>(StatusCodes.Status200OK);
        }
    }
}

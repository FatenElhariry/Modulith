using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);

    internal class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                Results.Ok(result.Adapt<DeleteProductResult>());
            }).WithName("Delete Product").
            Produces<DeleteProductResponse>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}

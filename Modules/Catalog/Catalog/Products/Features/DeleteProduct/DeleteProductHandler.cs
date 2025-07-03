using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>
    { }

    public record DeleteProductResult(bool Success, string Message);

    internal class DeleteProductHandler(CatalogDbContext dbContext) : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await dbContext.Products.Where(x => x.Id == request.ProductId).ExecuteDeleteAsync();

            return new DeleteProductResult(true, "");
        }
    }
}

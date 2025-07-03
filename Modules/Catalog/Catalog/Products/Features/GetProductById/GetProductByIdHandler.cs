using Catalog.Contract.Products.Features.GetProductById;

namespace Eshop.Catalog.Products.Features.GetProductById;
public class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.AsNoTracking().
            SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return new GetProductByIdResult(product.Adapt<ProductDto>());
    }
}


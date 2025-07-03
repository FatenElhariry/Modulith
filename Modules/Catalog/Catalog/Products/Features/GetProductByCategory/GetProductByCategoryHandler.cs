namespace Eshop.Catalog.Products.Features.GetProductByCategory
{
    public record GetProductByCategoryCommand(string Category) : IQuery<GetProductByCategoryResult>
    {

    }

    public record GetProductByCategoryResult(IEnumerable<ProductDto> Products)
    {
    }

    internal class GetProductByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByCategoryCommand, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryCommand request, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products.AsNoTracking().
                //Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Price, x.ImageUrl,  x.Category)).
                Where(x => x.Category.Contains(request.Category)).
                OrderBy(x => x.Name).
                ToListAsync(cancellationToken);

            var _products = products.Adapt<List<ProductDto>>();

            return new GetProductByCategoryResult(_products);
        }
    }
}

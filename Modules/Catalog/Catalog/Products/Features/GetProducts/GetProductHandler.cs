using EShop.Shared.Pagination;

namespace Eshop.Catalog.Products.Features.GetProducts
{
    public record class GetProductQuery(PaginationRequest Request) : IQuery<GetProductResult>
    {
    }

    public record GetProductResult(PaginatedResult<ProductDto> Products)
    {
       
    }

    internal class GetProductHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var pageIndex = request.Request.PageIndex;
            var pageSize = request.Request.PageSize;

            var totalCount = await dbContext.Products.CountAsync(cancellationToken);

            var products = await dbContext.Products.AsNoTracking().
                //Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Price, x.ImageUrl,  x.Category)).
                OrderBy(x => x.Name).
                Skip(pageIndex * pageSize).
                Take(pageSize).
                ToListAsync(cancellationToken);

            var _products = products.Adapt<List<ProductDto>>();

            return new GetProductResult(new PaginatedResult<ProductDto>(_products, totalCount, pageIndex, pageSize));
        }
    }
}

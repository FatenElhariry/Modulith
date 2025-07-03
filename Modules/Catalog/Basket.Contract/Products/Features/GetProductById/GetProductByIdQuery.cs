using Catalog.Contract.Products.Dtos;

namespace Catalog.Contract.Products.Features.GetProductById;
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(ProductDto Product);
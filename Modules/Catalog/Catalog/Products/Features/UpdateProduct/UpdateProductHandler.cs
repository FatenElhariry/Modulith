namespace Eshop.Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>
    {
    }

    public record UpdateProductResult(bool IsSuccess)
    {

    }

    internal class UpdateProductHandler (CatalogDbContext dbContext): ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(request.Product.Id, cancellationToken);
            if (product == null)
                throw new Exception($"Product not found: {request.Product.Id}.");

            product.Update( request.Product.Name,
                request.Product.Category,
                request.Product.Description,
                request.Product.Price,
                request.Product.ImageUrl);

            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}

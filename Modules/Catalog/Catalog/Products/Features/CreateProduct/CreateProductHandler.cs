using FluentValidation;
namespace Eshop.Catalog.Products.Features.CreateProduct;


public record CreateProductResult(Guid Id);
public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Product category is required.");
        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
        RuleFor(x => x.Product.ImageUrl).NotEmpty().WithMessage("Product image URL is required.");
    }
}

internal class CreateProductHandler(CatalogDbContext dbContext, 
    IValidator<CreateProductCommand> validator,
    ILogger<CreateProductCommand> logger) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //var result =  await validator.ValidateAsync(request, cancellationToken);
        //if (!result.IsValid)
        //{
        //    throw new ValidationException(result.Errors?.FirstOrDefault()?.ErrorMessage);
        //}


        logger.LogInformation("Creating product with name: {ProductName}", request.Product.Name);

        // Logic to create the product in the database
        // For now, we will just return a new Guid as the Id of the created product
        var prdocut = CreateProduct(request.Product);
        dbContext.Products.Add(prdocut);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(prdocut.Id);
    }

    private Product CreateProduct(ProductDto product)
    {
        return Product.Create(
            product.Name,
            product.Category,
            product.Description,
            product.Price,
            product.ImageUrl
        );
    }
}

using Eshop.Catalog.Products.Events;
using EShop.Shared.Domain;
using System.Runtime.CompilerServices;

namespace Eshop.Catalog.Products.Models
{
    public class Product : Aggregate<Guid>
    {
        public string Name { get; private set; } = default!;
        public List<string> Category { get; private set; } = new();
        public string Description { get; private set; } = default!;
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; } = default!;
        public static Product Create(string name, List<string> category, string description, decimal price, string imageUrl)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Category = category,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            product.DomainEvents.Add(new ProductCreatedEvent(product));

            return product;
        }
        public  void Update(string name, List<string> category, string description, decimal price, string imageUrl)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

            Name = name;
            Category = category;
            Description = description;
            ImageUrl = imageUrl;
            if (Price != price)
            {
                Price = price;
                this.AddDomainEvent(new ProductPriceChangeEvent(this));
            }
        }
    }
}

using EShop.Shared.Domain;
using System.Text.Json.Serialization;

namespace EShop.Basket.Basket.Models
{
    public class ShoppingCartItem : Entity<Guid>
    {
        [JsonConstructor]
        public ShoppingCartItem(Guid id, Guid shoppingCartId, Guid productId, string productName, decimal price, int quantity, string color): this(shoppingCartId, productId, productName, price, quantity, color)
        {
            Id = id;
        }
        public ShoppingCartItem(Guid shoppingCartId, Guid productId, string productName, decimal price, int quantity, string color)
        {
            ShoppingCartId = shoppingCartId;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            Color = color;

        }
        public Guid ShoppingCartId { get; private set; } = default;
        public Guid ProductId { get; private set; } = default;
        public string ProductName { get; private set; } = default!;
        public decimal Price { get; private set; }
        public int Quantity { get; internal set; }
        public string? Color { get; private set; } = default!;

       public void UpdatePrice(decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            Price = price;
        }
    }
}
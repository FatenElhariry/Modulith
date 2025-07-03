using EShop.Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Basket.Basket.Models
{
    public class ShoppingCart : Aggregate<Guid>
    {
        public string UserName { get; set; } = default;

        private readonly List<ShoppingCartItem> _items = new();

        public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);


        public static ShoppingCart Create(Guid Id, string userName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));

            var shoppingCart = new ShoppingCart
            {
                Id = Id,
                UserName = userName,
            };

            return shoppingCart;
        }

        public void RemoveItem(Guid productId)
        {
            var itemToRemove = _items.FirstOrDefault(x => x.ProductId == productId);
         
            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
            }
        }

        public void AddItem(Guid productId, string productName, decimal price, int quantity, string? color = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(productName, nameof(productName));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0, nameof(price));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0, nameof(quantity));

            var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new ShoppingCartItem(Id, productId, productName, price, quantity, color);
                _items.Add(newItem);
            }
        }
    }
}

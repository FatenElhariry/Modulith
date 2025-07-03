using EShop.Basket.Basket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Basket.Data.EntityTypeConfigurations
{
    internal class ShoppingCartItemTypeConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.ProductId).IsRequired();

            builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(1);

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();


        }
    }
}

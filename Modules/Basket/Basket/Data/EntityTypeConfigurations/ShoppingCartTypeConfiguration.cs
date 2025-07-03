
namespace EShop.Basket.Data.EntityTypeConfigurations
{
    internal class ShoppingCartTypeConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserName).IsUnique();

            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Items)
                   .WithOne()
                   .HasForeignKey(x => x.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

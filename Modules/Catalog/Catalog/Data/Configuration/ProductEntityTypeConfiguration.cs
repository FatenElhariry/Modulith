

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Catalog.Data.Configuration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");


            //    (entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            //    entity.Property(e => e.Description).HasMaxLength(500);
            //    entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            //    entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            //});

        }
    }
}

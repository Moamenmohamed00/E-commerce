using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(pv => pv.Id);


        builder.Property(pv => pv.Color)
            .HasMaxLength(50);

        builder.Property(pv => pv.Size)
            .HasMaxLength(50);

        builder.Property(pv => pv.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(pv => pv.StockQuantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(pv => pv.Product)
            .WithMany(p => p.Variants)
            .HasForeignKey(pv => pv.ProductId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasQueryFilter(pv => !pv.IsDeleted && !pv.Product.IsDeleted);
    }
}
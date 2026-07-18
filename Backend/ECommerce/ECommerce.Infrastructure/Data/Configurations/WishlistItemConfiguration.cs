using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
{
    public void Configure(EntityTypeBuilder<WishlistItem> builder)
    {
        builder.HasKey(wi => wi.Id);

        builder.HasIndex(wi => new { wi.UserId, wi.ProductVariantId })
            .IsUnique();

        builder.HasOne(wi => wi.User)
            .WithMany(u => u.WishlistItems)
            .HasForeignKey(wi => wi.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wi => wi.ProductVariant)
            .WithMany(pv => pv.WishlistItems)
            .HasForeignKey(wi => wi.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(wi => !wi.User.IsDeleted && !wi.ProductVariant.IsDeleted);
    }
}
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Quantity)
            .IsRequired()
            .HasDefaultValue(1); 


        builder.HasIndex(ci => new { ci.UserId, ci.ProductVariantId })
            .IsUnique();
        
        builder.HasOne(ci => ci.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ci => ci.ProductVariant)
            .WithMany(pv => pv.CartItems) 
            .HasForeignKey(ci => ci.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(ci => !ci.User.IsDeleted && !ci.ProductVariant.IsDeleted);
    }
}
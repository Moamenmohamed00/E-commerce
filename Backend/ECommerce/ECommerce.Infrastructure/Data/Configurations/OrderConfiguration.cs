using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(o => o.OrderStatus)
            .IsRequired()
            .HasConversion(
                status => status.ToString(), 
                value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value))
            .HasMaxLength(50);

      
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(o => o.Address)
            .WithMany(a => a.Orders) 
            .HasForeignKey(o => o.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(o => !o.Address.IsDeleted && !o.User.IsDeleted);
    }
}
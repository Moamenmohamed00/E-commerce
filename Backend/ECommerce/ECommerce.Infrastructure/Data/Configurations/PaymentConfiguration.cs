using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        
        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.PaymentStatus)
            .IsRequired()
            .HasConversion(
                status => status.ToString(),
                value => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), value))
            .HasMaxLength(50);

        builder.Property(p => p.TransactionId)
            .HasMaxLength(255); 
        builder.Property(p => p.PaymentMethod)
            .HasMaxLength(50);


       
        builder.HasOne(p => p.Order)
            .WithOne(o => o.Payment)
            .HasForeignKey<Payment>(p => p.OrderId) 
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
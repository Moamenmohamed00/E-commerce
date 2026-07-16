namespace ECommerce.Application.Features.Orders.DTOs;

public record OrderItemDto(
    int Id,
    int VariantId,
    string ProductName,
    string Color,
    string Size,
    int Quantity,
    decimal UnitPrice,
    string? PrimaryImageUrl
)
{
    public decimal TotalPrice => Quantity * UnitPrice;
}
namespace ECommerce.Application.Features.Cart.DTOs;

public record CartItemDto(
    int Id,
    int VariantId,
    int ProductId,
    string ProductName,
    string Color,
    string Size,
    decimal UnitPrice,
    int Quantity,
    int AvailableStock,
    string? PrimaryImageUrl)
{
    public decimal TotalPrice => UnitPrice * Quantity; 
}
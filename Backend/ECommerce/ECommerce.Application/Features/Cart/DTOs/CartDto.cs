namespace ECommerce.Application.Features.Cart.DTOs;

public record CartDto(List<CartItemDto> Items)
{
    public decimal GrandTotal => Items.Sum(i => i.TotalPrice);
}
namespace ECommerce.Application.Features.Orders.DTOs;

public record OrderDetailDto(
    int Id,
    DateTime OrderDate,
    decimal TotalAmount,
    string Status,
    string ShippingAddress,
    string PaymentMethod,
    List<OrderItemDto> Items
);
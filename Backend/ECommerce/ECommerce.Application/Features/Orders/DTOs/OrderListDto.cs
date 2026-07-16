namespace ECommerce.Application.Features.Orders.DTOs;

public record OrderListDto(
    int Id,
    DateTime OrderDate,
    decimal TotalAmount,
    string Status 
);
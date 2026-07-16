namespace ECommerce.Application.Features.Admin.Dashboard.DTOs;

public record RecentOrderDto(
    int OrderId,
    string CustomerName,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt
);
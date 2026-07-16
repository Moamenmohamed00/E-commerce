namespace ECommerce.Application.Features.Admin.Users.DTOs;

public record UserDetailDto(
    int Id,
    string FullName,
    string Email,
    string? PhoneNumber,
    bool IsActive,
    DateTime CreatedAt,
    int TotalOrders,     
    decimal TotalSpent   
);
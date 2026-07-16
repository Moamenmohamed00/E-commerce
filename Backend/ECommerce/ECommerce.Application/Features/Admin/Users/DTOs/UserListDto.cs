namespace ECommerce.Application.Features.Admin.Users.DTOs;

public record UserListDto(
    int Id,
    string FullName,
    string Email,
    bool IsActive,
    DateTime CreatedAt
);
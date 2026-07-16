namespace ECommerce.Application.Features.Admin.Users.DTOs;

public record PagedUsersResult(
    List<UserListDto> Items,
    int TotalCount,
    int PageNumber,
    int TotalPages
);
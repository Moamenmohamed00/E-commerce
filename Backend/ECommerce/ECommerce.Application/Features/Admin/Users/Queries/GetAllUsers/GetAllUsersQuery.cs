using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Admin.Users.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Admin.Users.Queries.GetAllUsers;

public record GetAllUsersQuery(
    string? SearchTerm, 
    bool? IsActive, 
    int PageNumber = 1, 
    int PageSize = 10
) : IRequest<Result<PagedUsersResult>>;
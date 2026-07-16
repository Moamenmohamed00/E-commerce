using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Admin.Users.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Admin.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<Result<UserDetailDto>>;
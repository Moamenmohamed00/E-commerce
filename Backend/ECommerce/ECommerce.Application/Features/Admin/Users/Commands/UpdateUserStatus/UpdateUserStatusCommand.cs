using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Admin.Users.Commands.UpdateUserStatus;

public record UpdateUserStatusCommand(int UserId, bool IsActive) : IRequest<Result>;
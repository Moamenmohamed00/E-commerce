using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Admin.Users.Commands.RevokeUserTokens;

public record RevokeUserTokensCommand(int UserId) : IRequest<Result>;
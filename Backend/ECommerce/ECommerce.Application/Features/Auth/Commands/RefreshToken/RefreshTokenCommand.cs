using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Auth.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<AuthTokensDto>>;
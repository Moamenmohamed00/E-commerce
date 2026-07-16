using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Admin.Users.Commands.RevokeUserTokens;

public class RevokeUserTokensCommandHandler : IRequestHandler<RevokeUserTokensCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RevokeUserTokensCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RevokeUserTokensCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
        {
            return Result.Failure("User not found.");
        }

        var activeTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == request.UserId && !rt.IsRevoked)
            .ToListAsync(cancellationToken);

        if (activeTokens.Any())
        {
            foreach (var token in activeTokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
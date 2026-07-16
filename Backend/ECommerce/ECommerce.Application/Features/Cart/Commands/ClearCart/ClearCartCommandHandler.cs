using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Cart.Commands.ClearCart;

public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ClearCartCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Unauthorized");

        var cartItems = await _context.CartItems
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);

        if (cartItems.Any())
        {
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
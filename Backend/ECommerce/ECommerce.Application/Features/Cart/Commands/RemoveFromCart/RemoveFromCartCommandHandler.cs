using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public RemoveFromCartCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Unauthorized");

        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == userId, cancellationToken);

        if (cartItem == null)
        {
            return Result.Failure("Cart item not found or you do not have permission to remove it.");
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
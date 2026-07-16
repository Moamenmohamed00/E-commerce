using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCartItemQuantityCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Unauthorized");

        var cartItem = await _context.CartItems
            .Include(c => c.ProductVariant)
            .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == userId, cancellationToken);

        if (cartItem == null)
            return Result.Failure("Cart item not found.");

        if (cartItem.ProductVariant == null || cartItem.ProductVariant.IsDeleted)
            return Result.Failure("This product is no longer available.");

        if (request.Quantity > cartItem.ProductVariant.StockQuantity)
            return Result.Failure($"Requested quantity exceeds available stock. Only {cartItem.ProductVariant.StockQuantity} available.");

        cartItem.Quantity = request.Quantity;
        cartItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
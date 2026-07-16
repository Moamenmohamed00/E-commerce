using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;

public class RemoveFromWishlistCommandHandler : IRequestHandler<RemoveFromWishlistCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public RemoveFromWishlistCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Unauthorized");

        var wishlistItem = await _context.WishlistItems
            .FirstOrDefaultAsync(w => w.Id == request.Id && w.UserId == userId, cancellationToken);

        if (wishlistItem == null)
        {
            return Result.Failure("Wishlist item not found or you do not have permission to remove it.");
        }

        _context.WishlistItems.Remove(wishlistItem);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
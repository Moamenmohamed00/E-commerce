using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Wishlist.Commands.AddToWishlist;

public class AddToWishlistCommandHandler : IRequestHandler<AddToWishlistCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AddToWishlistCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(AddToWishlistCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<int>.Failure("Unauthorized");

        var variantExists = await _context.ProductVariants
            .AnyAsync(v => v.Id == request.VariantId && !v.IsDeleted && !v.Product.IsDeleted, cancellationToken);

        if (!variantExists)
        {
            return Result<int>.Failure("This product is no longer available.");
        }

        var existingItem = await _context.WishlistItems
            .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductVariantId == request.VariantId, cancellationToken);

        if (existingItem != null)
        {
            return Result<int>.Success(existingItem.Id);
        }

        var wishlistItem = new WishlistItem
        {
            UserId = userId.Value,
            ProductVariantId = request.VariantId
        };

        _context.WishlistItems.Add(wishlistItem);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(wishlistItem.Id);
    }
}
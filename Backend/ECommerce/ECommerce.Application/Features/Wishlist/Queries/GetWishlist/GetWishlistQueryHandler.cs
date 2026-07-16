using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Wishlist.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Wishlist.Queries.GetWishlist;

public class GetWishlistQueryHandler : IRequestHandler<GetWishlistQuery, Result<IEnumerable<WishlistItemDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetWishlistQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<IEnumerable<WishlistItemDto>>> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null)
        {
            return Result<IEnumerable<WishlistItemDto>>.Failure("Unauthorized");
        }

        var wishlistItems = await _context.WishlistItems
            .AsNoTracking()
            .Where(w => w.UserId == userId 
                     && !w.ProductVariant.IsDeleted 
                     && !w.ProductVariant.Product.IsDeleted)
            .Select(w => new WishlistItemDto(
                w.Id,
                w.ProductVariantId,
                w.ProductVariant.ProductId,
                w.ProductVariant.Product.Name,
                w.ProductVariant.Color,
                w.ProductVariant.Size,
                w.ProductVariant.Price,
                w.ProductVariant.StockQuantity > 0,
                w.ProductVariant.Product.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault()
            ))
            .ToListAsync(cancellationToken);

        return Result<IEnumerable<WishlistItemDto>>.Success(wishlistItems);
    }
}
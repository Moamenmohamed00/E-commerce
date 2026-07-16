using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities; // بافتراض وجود كيان CartItem
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AddToCartCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<int>.Failure("Unauthorized");

        var variant = await _context.ProductVariants
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == request.VariantId && !v.IsDeleted && !v.Product.IsDeleted, cancellationToken);

        if (variant == null)
            return Result<int>.Failure("Product variant not found or is no longer available.");

        var existingCartItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductVariantId == request.VariantId, cancellationToken);

        if (existingCartItem != null)
        {
            // 3. مسار التحديث (Upsert): العنصر موجود، سنزيد الكمية
            var newTotalQuantity = existingCartItem.Quantity + request.Quantity;
            
            if (newTotalQuantity > variant.StockQuantity)
                return Result<int>.Failure($"Cannot add {request.Quantity} more. Only {variant.StockQuantity - existingCartItem.Quantity} left in stock.");

            existingCartItem.Quantity = newTotalQuantity;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(existingCartItem.Id);
        }
        else
        {
            if (request.Quantity > variant.StockQuantity)
                return Result<int>.Failure($"Requested quantity exceeds available stock. Only {variant.StockQuantity} available.");

            var cartItem = new CartItem
            {
                UserId = userId.Value, 
                ProductVariantId = request.VariantId,
                Quantity = request.Quantity
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(cartItem.Id);
        }
    }
}
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Cart.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Cart.Queries.GetCart;

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Result<CartDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetCartQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null)
        {
            return Result<CartDto>.Failure("User is not authenticated.");
        }

        var cartItems = await _context.CartItems
            .AsNoTracking()
            .Where(c => c.UserId == userId 
                     && !c.ProductVariant.IsDeleted 
                     && !c.ProductVariant.Product.IsDeleted)
            .Select(c => new CartItemDto(
                c.Id,
                c.ProductVariantId,
                c.ProductVariant.ProductId,
                c.ProductVariant.Product.Name,
                c.ProductVariant.Color,
                c.ProductVariant.Size,
                c.ProductVariant.Price,
                c.Quantity,
                c.ProductVariant.StockQuantity,
                c.ProductVariant.Product.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault()
            ))
            .ToListAsync(cancellationToken);


        var cartDto = new CartDto(cartItems);

        return Result<CartDto>.Success(cartDto);
    }
}
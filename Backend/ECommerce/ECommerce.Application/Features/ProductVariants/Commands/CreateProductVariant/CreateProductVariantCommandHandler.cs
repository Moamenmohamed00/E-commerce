using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductVariants.Commands.CreateProductVariant;

public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateProductVariantCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Result<int>> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.Id == request.ProductId && !p.IsDeleted, cancellationToken);
        if (!productExists) return Result<int>.Failure("Product not found.");

        var duplicateExists = await _context.ProductVariants
            .AnyAsync(v => v.ProductId == request.ProductId 
                        && v.Color.ToLower() == request.Color.ToLower() 
                        && v.Size.ToLower() == request.Size.ToLower() 
                        && !v.IsDeleted, cancellationToken);

        if (duplicateExists)
            return Result<int>.Failure("A variant with the same color and size already exists for this product.");

        var variant = new ProductVariant
        {
            ProductId = request.ProductId,
            Color = request.Color,
            Size = request.Size,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CreatedAt = DateTime.UtcNow
        };

        _context.ProductVariants.Add(variant);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(variant.Id);
    }
}
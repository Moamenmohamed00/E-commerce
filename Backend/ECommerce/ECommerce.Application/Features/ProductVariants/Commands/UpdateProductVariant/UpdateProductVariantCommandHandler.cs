using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductVariants.Commands.UpdateProductVariant;

public class UpdateProductVariantCommandHandler : IRequestHandler<UpdateProductVariantCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductVariantCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(v => v.Id == request.Id && v.ProductId == request.ProductId && !v.IsDeleted, cancellationToken);

        if (variant == null) return Result.Failure("Variant not found.");

        var duplicateExists = await _context.ProductVariants
            .AnyAsync(v => v.Id != request.Id 
                        && v.ProductId == request.ProductId
                        && v.Color.ToLower() == request.Color.ToLower() 
                        && v.Size.ToLower() == request.Size.ToLower() 
                        && !v.IsDeleted, cancellationToken);

        if (duplicateExists) return Result.Failure("Another variant with the same color and size already exists.");

        variant.Color = request.Color;
        variant.Size = request.Size;
        variant.Price = request.Price;
        variant.StockQuantity = request.StockQuantity;
        variant.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
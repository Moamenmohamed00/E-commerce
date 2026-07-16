using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductImages.Commands.AddProductImage;

public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public AddProductImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.Id == request.ProductId && !p.IsDeleted, cancellationToken);

        if (!productExists) return Result<int>.Failure("Product not found.");

        var hasExistingImages = await _context.ProductImages
            .AnyAsync(i => i.ProductId == request.ProductId, cancellationToken);

        bool isPrimary = request.IsPrimary || !hasExistingImages; 

        if (isPrimary && hasExistingImages)
        {
            var existingPrimaryImages = await _context.ProductImages
                .Where(i => i.ProductId == request.ProductId && i.IsPrimary)
                .ToListAsync(cancellationToken);

            foreach (var img in existingPrimaryImages)
            {
                img.IsPrimary = false;
            }
        }

        var image = new ProductImage
        {
            ProductId = request.ProductId,
            ImageUrl = request.ImageUrl,
            PublicId = request.PublicId,
            IsPrimary = isPrimary,
            CreatedAt = DateTime.UtcNow
        };

        _context.ProductImages.Add(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(image.Id);
    }
}
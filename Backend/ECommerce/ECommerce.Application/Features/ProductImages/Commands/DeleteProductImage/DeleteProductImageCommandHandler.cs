using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductImages.Commands.DeleteProductImage;

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductImageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.ProductImages
            .FirstOrDefaultAsync(i => i.Id == request.Id && i.ProductId == request.ProductId, cancellationToken);

        if (image == null) return Result.Failure("Image not found.");

        var imageCount = await _context.ProductImages
            .CountAsync(i => i.ProductId == request.ProductId, cancellationToken);

        if (imageCount == 1)
        {
            return Result.Failure("Cannot delete the only image of the product.");
        }

        if (image.IsPrimary)
        {
            var nextImage = await _context.ProductImages
                .Where(i => i.ProductId == request.ProductId && i.Id != request.Id)
                .OrderBy(i => i.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (nextImage != null) nextImage.IsPrimary = true;
        }

        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
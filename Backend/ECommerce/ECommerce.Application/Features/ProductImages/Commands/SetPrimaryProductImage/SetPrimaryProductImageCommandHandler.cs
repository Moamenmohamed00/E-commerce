using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductImages.Commands.SetPrimaryProductImage;

public class SetPrimaryProductImageCommandHandler : IRequestHandler<SetPrimaryProductImageCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public SetPrimaryProductImageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(SetPrimaryProductImageCommand request, CancellationToken cancellationToken)
    {
        var images = await _context.ProductImages
            .Where(i => i.ProductId == request.ProductId)
            .ToListAsync(cancellationToken);

        var targetImage = images.FirstOrDefault(i => i.Id == request.Id);
        
        if (targetImage == null) return Result.Failure("Image not found for this product.");
        if (targetImage.IsPrimary) return Result.Success(); // هي بالفعل رئيسية

        foreach (var img in images)
        {
            img.IsPrimary = (img.Id == request.Id);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
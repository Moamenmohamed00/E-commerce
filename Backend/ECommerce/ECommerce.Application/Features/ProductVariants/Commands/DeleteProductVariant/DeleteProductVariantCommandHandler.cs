using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductVariants.Commands.DeleteProductVariant;

public class DeleteProductVariantCommandHandler : IRequestHandler<DeleteProductVariantCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductVariantCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Result> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(v => v.Id == request.Id && v.ProductId == request.ProductId && !v.IsDeleted, cancellationToken);

        if (variant == null) return Result.Failure("Variant not found.");

        variant.IsDeleted = true;
        variant.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
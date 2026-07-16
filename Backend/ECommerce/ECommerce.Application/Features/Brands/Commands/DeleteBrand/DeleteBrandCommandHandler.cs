using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteBrandCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(b => b.Id == request.Id && !b.IsDeleted, cancellationToken);

        if (brand == null)
        {
            return Result.Failure("Brand not found.");
        }

        var hasProducts = await _context.Products
            .AnyAsync(p => p.BrandId == request.Id && !p.IsDeleted, cancellationToken);

        if (hasProducts)
        {
            return Result.Failure("Cannot delete this brand because it has active products associated with it. Please reassign or delete the products first.");
        }

        brand.IsDeleted = true;
        brand.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
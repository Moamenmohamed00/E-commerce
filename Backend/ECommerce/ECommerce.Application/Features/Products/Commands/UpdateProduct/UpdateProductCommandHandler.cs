using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id && !p.IsDeleted, cancellationToken);

        if (product == null)
        {
            return Result.Failure("Product not found.");
        }

        if (product.CategoryId != request.CategoryId)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == request.CategoryId && !c.IsDeleted, cancellationToken);
                
            if (!categoryExists) return Result.Failure("The specified category does not exist.");
        }

        if (product.BrandId != request.BrandId)
        {
            var brandExists = await _context.Brands
                .AnyAsync(b => b.Id == request.BrandId && !b.IsDeleted, cancellationToken);
                
            if (!brandExists) return Result.Failure("The specified brand does not exist.");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.CategoryId = request.CategoryId;
        product.BrandId = request.BrandId;
        product.IsActive = request.IsActive;
        product.UpdatedAt = DateTime.UtcNow; 

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
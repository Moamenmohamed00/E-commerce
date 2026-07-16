using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // 1. التحقق من وجود القسم
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == request.CategoryId && !c.IsDeleted, cancellationToken);
            
        if (!categoryExists)
        {
            return Result<int>.Failure("The specified category does not exist.");
        }

        // 2. التحقق من وجود العلامة التجارية (أصبحت إجبارية الآن)
        var brandExists = await _context.Brands
            .AnyAsync(b => b.Id == request.BrandId && !b.IsDeleted, cancellationToken);
            
        if (!brandExists)
        {
            return Result<int>.Failure("The specified brand does not exist.");
        }

        // 3. إنشاء المنتج الأساسي
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId,
            BrandId = request.BrandId,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow 
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(product.Id);
    }
}
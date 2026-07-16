using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentCategoryId.HasValue && request.ParentCategoryId.Value == request.Id)
        {
            return Result.Failure("A category cannot be its own parent.");
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, cancellationToken);

        if (category == null)
        {
            return Result.Failure("Category not found.");
        }

        if (request.ParentCategoryId.HasValue && request.ParentCategoryId != category.ParentCategoryId)
        {
            var parentExists = await _context.Categories
                .AnyAsync(c => c.Id == request.ParentCategoryId.Value && !c.IsDeleted, cancellationToken);
                
            if (!parentExists)
            {
                return Result.Failure("The specified parent category does not exist.");
            }
        }

        var nameExists = await _context.Categories
            .AnyAsync(c => c.Name == request.Name 
                        && c.ParentCategoryId == request.ParentCategoryId 
                        && c.Id != request.Id 
                        && !c.IsDeleted, cancellationToken);

        if (nameExists)
        {
            return Result.Failure("A category with the same name already exists in this level.");
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.ParentCategoryId = request.ParentCategoryId;
        category.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
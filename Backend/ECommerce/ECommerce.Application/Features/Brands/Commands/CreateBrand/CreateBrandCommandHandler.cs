using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateBrandCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Result<int>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var nameExists = await _context.Brands
            .AnyAsync(b => b.Name == request.Name && !b.IsDeleted, cancellationToken);

        if (nameExists) return Result<int>.Failure("A brand with this name already exists.");

        var brand = new Brand
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(brand.Id);
    }
}
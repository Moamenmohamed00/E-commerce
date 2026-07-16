using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Products.DTOs;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductImages.Queries.GetProductImagesByProductId;

public class GetProductImagesByProductIdQueryHandler : IRequestHandler<GetProductImagesByProductIdQuery, Result<IEnumerable<ProductImageDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductImagesByProductIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProductImageDto>>> Handle(GetProductImagesByProductIdQuery request, CancellationToken cancellationToken)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.Id == request.ProductId && !p.IsDeleted, cancellationToken);

        if (!productExists)
        {
            return Result<IEnumerable<ProductImageDto>>.Failure("Product not found.");
        }

        var images = await _context.ProductImages
            .AsNoTracking()
            .Where(i => i.ProductId == request.ProductId)
            .OrderByDescending(i => i.IsPrimary)
            .ThenByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);

        var imageDtos = _mapper.Map<IEnumerable<ProductImageDto>>(images);

        return Result<IEnumerable<ProductImageDto>>.Success(imageDtos);
    }
}
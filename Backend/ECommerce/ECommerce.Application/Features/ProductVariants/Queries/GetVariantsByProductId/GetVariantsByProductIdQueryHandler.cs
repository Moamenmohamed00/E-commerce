using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Products.DTOs;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.ProductVariants.Queries.GetVariantsByProductId;

public class GetVariantsByProductIdQueryHandler : IRequestHandler<GetVariantsByProductIdQuery, Result<IEnumerable<ProductVariantDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVariantsByProductIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProductVariantDto>>> Handle(GetVariantsByProductIdQuery request, CancellationToken cancellationToken)
    {
        var variants = await _context.ProductVariants
            .AsNoTracking()
            .Where(v => v.ProductId == request.ProductId && !v.IsDeleted)
            .OrderBy(v => v.Color)
            .ThenBy(v => v.Size)
            .ToListAsync(cancellationToken);

        var variantDtos = _mapper.Map<IEnumerable<ProductVariantDto>>(variants);
        return Result<IEnumerable<ProductVariantDto>>.Success(variantDtos);
    }
}
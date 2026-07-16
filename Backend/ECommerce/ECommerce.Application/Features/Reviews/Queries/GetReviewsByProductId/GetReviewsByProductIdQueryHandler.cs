using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Reviews.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Reviews.Queries.GetReviewsByProductId;

public class GetReviewsByProductIdQueryHandler : IRequestHandler<GetReviewsByProductIdQuery, Result<IEnumerable<ProductReviewDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetReviewsByProductIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<ProductReviewDto>>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews 
            .AsNoTracking()
            .Where(r => r.ProductId == request.ProductId && !r.IsDeleted)
            .OrderByDescending(r => r.CreatedAt) 
            .Select(r => new ProductReviewDto(
                r.Id,
                // بافتراض أن كيان الـ User يحتوي على خاصية Name أو FirstName
                r.User != null ? r.User.FirstName + r.User.LastName : "Anonymous", 
                r.Rating,
                r.Comment,
                r.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return Result<IEnumerable<ProductReviewDto>>.Success(reviews);
    }
}
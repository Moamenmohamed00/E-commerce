using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Reviews.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Reviews.Queries.GetUserReviews;

public class GetUserReviewsQueryHandler : IRequestHandler<GetUserReviewsQuery, Result<IEnumerable<UserReviewDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetUserReviewsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<IEnumerable<UserReviewDto>>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<IEnumerable<UserReviewDto>>.Failure("Unauthorized");

        var reviews = await _context.Reviews
            .AsNoTracking()
            .Where(r => r.UserId == userId && !r.IsDeleted) 
            .OrderByDescending(r => r.CreatedAt) 
            .Select(r => new UserReviewDto(
                r.Id,
                r.ProductId,
                r.Product.Name,
                r.Product.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault(),
                r.Rating,
                r.Comment,
                r.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return Result<IEnumerable<UserReviewDto>>.Success(reviews);
    }
}
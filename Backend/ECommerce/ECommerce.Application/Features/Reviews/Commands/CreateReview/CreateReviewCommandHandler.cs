using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateReviewCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<int>.Failure("Unauthorized");

        var existingReview = await _context.Reviews
            .AnyAsync(r => r.ProductId == request.ProductId && r.UserId == userId && !r.IsDeleted, cancellationToken);

        if (existingReview)
        {
            return Result<int>.Failure("You have already reviewed this product.");
        }

        var hasPurchased = await _context.Orders
            .Where(o => o.UserId == userId && o.OrderStatus.ToString() == "Delivered") 
            .SelectMany(o => o.OrderItems) 
            .AnyAsync(oi => oi.ProductVariant.ProductId == request.ProductId, cancellationToken);

        if (!hasPurchased)
        {
            return Result<int>.Failure("You can only review products that you have purchased and received.");
        }

        var review = new Review
        {
            UserId = userId.Value,
            ProductId = request.ProductId,
            Rating = request.Rating,
            Comment = request.Comment,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(review.Id);
    }
}
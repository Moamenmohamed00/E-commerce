using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateReviewCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Unauthorized");

        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == request.Id && r.UserId == userId && !r.IsDeleted, cancellationToken);

        if (review == null)
        {
            return Result.Failure("Review not found or you do not have permission to update it.");
        }

        review.Rating = request.Rating;
        review.Comment = request.Comment;
        
         review.UpdatedAt = DateTime.UtcNow; 

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
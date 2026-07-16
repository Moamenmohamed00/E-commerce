using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Reviews.Commands.CreateReview;

public record CreateReviewCommand(
    int ProductId, 
    int Rating, 
    string Comment) : IRequest<Result<int>>;
using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Reviews.Commands.UpdateReview;

public record UpdateReviewCommand(
    int Id,
    int Rating, 
    string Comment) : IRequest<Result>;
using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand(int Id) : IRequest<Result>;
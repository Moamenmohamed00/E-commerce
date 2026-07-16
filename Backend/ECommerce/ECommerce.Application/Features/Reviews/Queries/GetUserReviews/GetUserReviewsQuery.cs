using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Reviews.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Reviews.Queries.GetUserReviews;

public record GetUserReviewsQuery() : IRequest<Result<IEnumerable<UserReviewDto>>>;
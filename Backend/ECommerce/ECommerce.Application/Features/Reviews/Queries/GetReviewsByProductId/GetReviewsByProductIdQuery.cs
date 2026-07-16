using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Reviews.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Reviews.Queries.GetReviewsByProductId;

public record GetReviewsByProductIdQuery(int ProductId) : IRequest<Result<IEnumerable<ProductReviewDto>>>;
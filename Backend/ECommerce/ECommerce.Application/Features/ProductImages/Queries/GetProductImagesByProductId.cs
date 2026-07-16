using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Features.ProductImages.Queries.GetProductImagesByProductId;

public record GetProductImagesByProductIdQuery(int ProductId) : IRequest<Result<IEnumerable<ProductImageDto>>>;
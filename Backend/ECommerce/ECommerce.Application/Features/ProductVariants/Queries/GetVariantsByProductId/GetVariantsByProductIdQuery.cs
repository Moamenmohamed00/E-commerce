using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Features.ProductVariants.Queries.GetVariantsByProductId;

public record GetVariantsByProductIdQuery(int ProductId) : IRequest<Result<IEnumerable<ProductVariantDto>>>;
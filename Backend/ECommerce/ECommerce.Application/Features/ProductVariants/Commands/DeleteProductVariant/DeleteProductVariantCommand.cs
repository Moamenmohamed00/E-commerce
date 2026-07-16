using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.ProductVariants.Commands.DeleteProductVariant;

public record DeleteProductVariantCommand(int Id, int ProductId) : IRequest<Result>;
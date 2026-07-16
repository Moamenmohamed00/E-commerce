using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.ProductVariants.Commands.UpdateStock;

public record UpdateStockCommand(
    int VariantId, 
    int ProductId, 
    int NewStockQuantity) : IRequest<Result>;
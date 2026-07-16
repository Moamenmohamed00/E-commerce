using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.ProductVariants.Commands.UpdateProductVariant;

public record UpdateProductVariantCommand(
    int Id, 
    int ProductId, 
    string Color, 
    string Size, 
    decimal Price, 
    int StockQuantity) : IRequest<Result>;
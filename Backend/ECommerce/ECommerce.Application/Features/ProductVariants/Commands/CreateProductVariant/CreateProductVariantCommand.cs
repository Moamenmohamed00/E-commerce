using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.ProductVariants.Commands.CreateProductVariant;

public record CreateProductVariantCommand(
    int ProductId, 
    string Color, 
    string Size, 
    decimal Price, 
    int StockQuantity) : IRequest<Result<int>>;
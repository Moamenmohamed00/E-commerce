using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Products.DTOs;

public record ProductVariantDto(
    int Id, 
    string Color, 
    string Size, 
    decimal Price, 
    int StockQuantity) : IMapFrom<ProductVariant>;
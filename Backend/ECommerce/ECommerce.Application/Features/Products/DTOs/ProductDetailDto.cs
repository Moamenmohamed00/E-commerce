using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Products.DTOs;

public record ProductDetailDto(
    int Id, 
    string Name, 
    string Description, 
    bool IsActive,
    int CategoryId,
    string CategoryName, 
    int BrandId,
    string BrandName,
    List<ProductImageDto> Images,
    List<ProductVariantDto> Variants) : IMapFrom<Product>;
using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Products.DTOs;

public record ProductImageDto(
    int Id, 
    string ImageUrl, 
    bool IsPrimary) : IMapFrom<ProductImage>;
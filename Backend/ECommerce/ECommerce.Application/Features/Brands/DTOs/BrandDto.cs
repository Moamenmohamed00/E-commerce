using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Brands.DTOs;

public record BrandDto(
    int Id, 
    string Name, 
    string Description, 
    string LogoUrl) : IMapFrom<Brand>;
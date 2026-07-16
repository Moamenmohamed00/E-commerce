using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Categories.DTOs;

public record CategoryDto(
    int Id, 
    string Name, 
    string Description, 
    int? ParentCategoryId,
    List<CategoryDto> SubCategories) : IMapFrom<Category>;
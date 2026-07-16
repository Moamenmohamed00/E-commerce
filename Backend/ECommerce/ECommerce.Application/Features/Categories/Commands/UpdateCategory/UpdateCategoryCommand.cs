using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    int Id, 
    string Name, 
    string Description, 
    int? ParentCategoryId) : IRequest<Result>;
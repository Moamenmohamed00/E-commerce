using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest<Result>;
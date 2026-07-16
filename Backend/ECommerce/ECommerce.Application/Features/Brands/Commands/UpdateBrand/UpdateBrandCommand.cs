using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Brands.Commands.UpdateBrand;

public record UpdateBrandCommand(
    int Id, 
    string Name, 
    string Description) : IRequest<Result>;
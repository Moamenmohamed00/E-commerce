using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Brands.Commands.CreateBrand;

public record CreateBrandCommand(string Name, string Description) : IRequest<Result<int>>;
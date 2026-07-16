using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Brands.Commands.DeleteBrand;

public record DeleteBrandCommand(int Id) : IRequest<Result>;
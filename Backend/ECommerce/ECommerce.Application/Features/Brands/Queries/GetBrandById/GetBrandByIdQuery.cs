using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Brands.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Brands.Queries.GetBrandById;

public record GetBrandByIdQuery(int Id) : IRequest<Result<BrandDto>>;
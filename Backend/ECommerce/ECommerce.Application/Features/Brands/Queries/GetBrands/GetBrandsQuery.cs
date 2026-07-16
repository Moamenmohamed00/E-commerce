// GetBrandsQuery.cs
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Brands.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Brands.Queries.GetBrands;

public record GetBrandsQuery() : IRequest<Result<IEnumerable<BrandDto>>>;

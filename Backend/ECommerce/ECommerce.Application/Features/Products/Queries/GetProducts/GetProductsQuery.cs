using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Products.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    int? CategoryId = null,
    int? BrandId = null,
    string? SortBy = null // أمثلة: "price_asc", "price_desc", "name", "newest"
) : IRequest<Result<PaginatedList<ProductListDto>>>;
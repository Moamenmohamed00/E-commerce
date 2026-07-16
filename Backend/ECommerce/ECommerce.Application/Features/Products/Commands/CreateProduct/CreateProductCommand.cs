using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name, 
    string Description, 
    int CategoryId, 
    int BrandId,
    bool IsActive = true) : IRequest<Result<int>>;
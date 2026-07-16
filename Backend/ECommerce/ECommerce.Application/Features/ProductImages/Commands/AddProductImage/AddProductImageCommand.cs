using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.ProductImages.Commands.AddProductImage;

public record AddProductImageCommand(
    int ProductId, 
    string ImageUrl, 
    string PublicId, 
    bool IsPrimary) : IRequest<Result<int>>;
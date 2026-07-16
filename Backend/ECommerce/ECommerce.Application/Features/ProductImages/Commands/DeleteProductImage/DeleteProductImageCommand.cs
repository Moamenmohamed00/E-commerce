using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.ProductImages.Commands.DeleteProductImage;

public record DeleteProductImageCommand(int Id, int ProductId) : IRequest<Result>;
using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.ProductImages.Commands.SetPrimaryProductImage;

public record SetPrimaryProductImageCommand(int Id, int ProductId) : IRequest<Result>;
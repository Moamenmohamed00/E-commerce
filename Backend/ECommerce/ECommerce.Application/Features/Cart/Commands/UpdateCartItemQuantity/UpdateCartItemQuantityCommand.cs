using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public record UpdateCartItemQuantityCommand(int Id, int Quantity) : IRequest<Result>;
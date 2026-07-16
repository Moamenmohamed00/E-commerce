using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart;

public record RemoveFromCartCommand(int Id) : IRequest<Result>;
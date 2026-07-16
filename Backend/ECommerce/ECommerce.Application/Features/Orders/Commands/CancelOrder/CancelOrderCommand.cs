using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.CancelOrder;

public record CancelOrderCommand(int OrderId) : IRequest<Result>;

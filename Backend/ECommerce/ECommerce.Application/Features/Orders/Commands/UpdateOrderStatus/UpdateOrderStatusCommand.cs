using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(int OrderId, string NewStatus) : IRequest<Result>;
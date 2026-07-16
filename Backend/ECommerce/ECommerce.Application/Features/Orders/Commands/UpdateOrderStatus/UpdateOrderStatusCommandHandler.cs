using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateOrderStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductVariant)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order == null)
            return Result.Failure("Order not found.");

        if (!Enum.TryParse<OrderStatus>(request.NewStatus, true, out var parsedStatus))
        {
            return Result.Failure("Invalid order status.");
        }

        var isAlreadyCancelled = order.OrderStatus.ToString() == "Cancelled";
        var isCancellingNow = request.NewStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase);

        if (isCancellingNow && !isAlreadyCancelled)
        {
            foreach (var item in order.OrderItems)
            {
                if (item.ProductVariant != null)
                {
                    item.ProductVariant.StockQuantity += item.Quantity;
                }
            }
        }

        order.OrderStatus = parsedStatus; 

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
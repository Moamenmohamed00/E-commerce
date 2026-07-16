using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CancelOrderCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Unauthorized");

        var order = await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductVariant)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == userId, cancellationToken);

        if (order == null)
            return Result.Failure("Order not found or you do not have permission to cancel it.");

        var currentStatus = order.OrderStatus.ToString();
        if (currentStatus == "Shipped" || currentStatus == "Delivered")
        {
            return Result.Failure("Order cannot be cancelled because it has already been shipped or delivered.");
        }
        
        if (currentStatus == "Cancelled")
        {
            return Result.Failure("Order is already cancelled.");
        }

         order.OrderStatus = OrderStatus.Cancelled; 
        

        foreach (var item in order.OrderItems)
        {
            if (item.ProductVariant != null)
            {
                item.ProductVariant.StockQuantity += item.Quantity;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
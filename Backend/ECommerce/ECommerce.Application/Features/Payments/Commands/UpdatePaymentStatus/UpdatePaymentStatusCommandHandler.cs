using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Payments.Commands.UpdatePaymentStatus;

public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdatePaymentStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .Include(p => p.Order)
                .ThenInclude(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
            .FirstOrDefaultAsync(p => p.OrderId == request.OrderId, cancellationToken);

        if (payment == null)
        {
            return Result.Failure("Payment record not found for this order.");
        }


        if (payment.PaymentStatus == PaymentStatus.Paid)
        {
            return Result.Success(); 
        }

        payment.TransactionId = request.TransactionId;
        payment.UpdatedAt = DateTime.UtcNow;

        var newStatus = request.Status.ToUpper();

        if (newStatus == "COMPLETED" || newStatus == "SUCCESS")
        {
             payment.PaymentStatus = PaymentStatus.Paid;
             payment.Order.OrderStatus = OrderStatus.Processing;
        }
        else if (newStatus == "FAILED" || newStatus == "DECLINED")
        {
             payment.PaymentStatus = PaymentStatus.Failed;
             payment.Order.OrderStatus = OrderStatus.Cancelled;

            foreach (var item in payment.Order.OrderItems)
            {
                if (item.ProductVariant != null)
                {
                    item.ProductVariant.StockQuantity += item.Quantity;
                }
            }
        }
        else
        {
            return Result.Failure("Unknown payment status received.");
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
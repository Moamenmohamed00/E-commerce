using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Payments.Commands.UpdatePaymentStatus;

public record UpdatePaymentStatusCommand(
    int OrderId, 
    string TransactionId, 
    string Status 
) : IRequest<Result>;
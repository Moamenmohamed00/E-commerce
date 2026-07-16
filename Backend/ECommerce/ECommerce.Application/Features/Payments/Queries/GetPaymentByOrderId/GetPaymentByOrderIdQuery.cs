using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Payments.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Payments.Queries.GetPaymentByOrderId;

public record GetPaymentByOrderIdQuery(int OrderId) : IRequest<Result<PaymentDto>>;
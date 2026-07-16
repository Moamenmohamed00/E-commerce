using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Payments.Commands.CreatePaymentIntent;

public record CreatePaymentIntentCommand(int OrderId) : IRequest<Result<string>>;

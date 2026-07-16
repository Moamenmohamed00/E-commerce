using FluentValidation;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(v => v.AddressId).GreaterThan(0).WithMessage("Please select a valid shipping address.");
        
        RuleFor(v => v.PaymentMethod)
            .NotEmpty().WithMessage("Payment method is required.")
            .MaximumLength(50);
    }
}
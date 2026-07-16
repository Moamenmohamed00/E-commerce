using FluentValidation;

namespace ECommerce.Application.Features.Payments.Commands.CreatePaymentIntent;

public class CreatePaymentIntentCommandValidator : AbstractValidator<CreatePaymentIntentCommand>
{
    public CreatePaymentIntentCommandValidator()
    {
        RuleFor(v => v.OrderId)
            .GreaterThan(0).WithMessage("Order Id must be greater than 0.");
    }
}

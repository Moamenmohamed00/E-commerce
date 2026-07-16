using FluentValidation;

namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommandValidator : AbstractValidator<RemoveFromCartCommand>
{
    public RemoveFromCartCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Cart Item Id must be greater than 0.");
    }
}
using FluentValidation;
namespace ECommerce.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(v => v.OrderId).GreaterThan(0).WithMessage("Order Id must be greater than 0.");
    }
}
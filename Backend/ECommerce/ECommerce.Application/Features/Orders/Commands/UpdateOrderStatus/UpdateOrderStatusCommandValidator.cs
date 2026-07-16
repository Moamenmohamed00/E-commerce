using FluentValidation;
namespace ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(v => v.OrderId).GreaterThan(0);
        RuleFor(v => v.NewStatus).NotEmpty().WithMessage("New status is required.");
    }
}
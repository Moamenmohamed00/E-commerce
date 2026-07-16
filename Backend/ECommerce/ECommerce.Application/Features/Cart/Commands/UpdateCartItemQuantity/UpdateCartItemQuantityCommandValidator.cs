using FluentValidation;

namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public class UpdateCartItemQuantityCommandValidator : AbstractValidator<UpdateCartItemQuantityCommand>
{
    public UpdateCartItemQuantityCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        
        RuleFor(v => v.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1. If you want to remove the item, use the Remove endpoint.")
            .LessThanOrEqualTo(50).WithMessage("Maximum quantity allowed per item is 50.");
    }
}
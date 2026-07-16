using FluentValidation;

namespace ECommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(v => v.VariantId).GreaterThan(0);
        
        RuleFor(v => v.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1.")
            .LessThanOrEqualTo(50).WithMessage("You cannot add more than 50 items of the same product at once."); // حماية من الـ Abuse
    }
}
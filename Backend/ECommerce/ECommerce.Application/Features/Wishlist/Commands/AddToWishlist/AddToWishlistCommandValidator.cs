using FluentValidation;

namespace ECommerce.Application.Features.Wishlist.Commands.AddToWishlist;

public class AddToWishlistCommandValidator : AbstractValidator<AddToWishlistCommand>
{
    public AddToWishlistCommandValidator()
    {
        RuleFor(v => v.VariantId)
            .GreaterThan(0).WithMessage("Variant Id must be greater than 0.");
    }
}
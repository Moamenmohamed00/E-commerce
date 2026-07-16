using FluentValidation;

namespace ECommerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;

public class RemoveFromWishlistCommandValidator : AbstractValidator<RemoveFromWishlistCommand>
{
    public RemoveFromWishlistCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Wishlist Item Id must be greater than 0.");
    }
}
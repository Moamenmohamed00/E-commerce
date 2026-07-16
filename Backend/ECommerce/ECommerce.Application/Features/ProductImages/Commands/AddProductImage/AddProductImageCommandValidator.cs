using FluentValidation;

namespace ECommerce.Application.Features.ProductImages.Commands.AddProductImage;

public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
{
    public AddProductImageCommandValidator()
    {
        RuleFor(v => v.ProductId).GreaterThan(0);
        RuleFor(v => v.ImageUrl).NotEmpty();
        RuleFor(v => v.PublicId).NotEmpty();
    }
}
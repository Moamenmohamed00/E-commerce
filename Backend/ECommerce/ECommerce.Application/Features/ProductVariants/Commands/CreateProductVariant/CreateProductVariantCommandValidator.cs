using FluentValidation;

namespace ECommerce.Application.Features.ProductVariants.Commands.CreateProductVariant;

public class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand>
{
    public CreateProductVariantCommandValidator()
    {
        RuleFor(v => v.ProductId).GreaterThan(0);
        RuleFor(v => v.Color).MaximumLength(50);
        RuleFor(v => v.Size).MaximumLength(50);
        RuleFor(v => v.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(v => v.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
    }
}
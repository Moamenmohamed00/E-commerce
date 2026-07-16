using FluentValidation;

namespace ECommerce.Application.Features.ProductVariants.Commands.UpdateProductVariant;

public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.ProductId).GreaterThan(0);
        RuleFor(v => v.Color).MaximumLength(50);
        RuleFor(v => v.Size).MaximumLength(50);
        RuleFor(v => v.Price).GreaterThan(0);
        RuleFor(v => v.StockQuantity).GreaterThanOrEqualTo(0);
    }
}
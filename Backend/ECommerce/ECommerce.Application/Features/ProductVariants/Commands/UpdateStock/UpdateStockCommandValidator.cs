using FluentValidation;

namespace ECommerce.Application.Features.ProductVariants.Commands.UpdateStock;

public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockCommandValidator()
    {
        RuleFor(v => v.VariantId).GreaterThan(0);
        RuleFor(v => v.ProductId).GreaterThan(0);
        
        RuleFor(v => v.NewStockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");
    }
}
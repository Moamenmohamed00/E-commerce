using FluentValidation;

namespace ECommerce.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Brand Id must be greater than 0.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Brand name is required.")
            .MaximumLength(100).WithMessage("Brand name must not exceed 100 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(500);
    }
}
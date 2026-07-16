using FluentValidation;

namespace ECommerce.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Brand Id must be greater than 0.");
    }
}
using FluentValidation;

namespace ECommerce.Application.Features.Brands.Queries.GetBrandById;

public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Brand Id must be greater than 0.");
    }
}
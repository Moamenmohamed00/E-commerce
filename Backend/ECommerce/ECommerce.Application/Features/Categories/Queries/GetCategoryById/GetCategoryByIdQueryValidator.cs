using FluentValidation;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Category Id must be greater than 0.");
    }
}
using FluentValidation;

namespace ECommerce.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Product Id must be greater than 0.");
    }
}
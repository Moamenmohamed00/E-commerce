using FluentValidation;

namespace ECommerce.Application.Features.Reviews.Queries.GetReviewsByProductId;

public class GetReviewsByProductIdQueryValidator : AbstractValidator<GetReviewsByProductIdQuery>
{
    public GetReviewsByProductIdQueryValidator()
    {
        RuleFor(v => v.ProductId)
            .GreaterThan(0).WithMessage("Product Id must be greater than 0.");
    }
}
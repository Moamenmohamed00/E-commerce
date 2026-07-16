using FluentValidation;

namespace ECommerce.Application.Features.ProductImages.Queries.GetProductImagesByProductId;

public class GetProductImagesByProductIdQueryValidator : AbstractValidator<GetProductImagesByProductIdQuery>
{
    public GetProductImagesByProductIdQueryValidator()
    {
        RuleFor(v => v.ProductId)
            .GreaterThan(0).WithMessage("Product Id must be greater than 0.");
    }
}
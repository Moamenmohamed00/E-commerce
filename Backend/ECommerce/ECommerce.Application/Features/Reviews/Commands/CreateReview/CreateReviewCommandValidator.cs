using FluentValidation;

namespace ECommerce.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(v => v.ProductId)
            .GreaterThan(0).WithMessage("Product Id must be greater than 0.");

        RuleFor(v => v.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(v => v.Comment)
            .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.");
    }
}
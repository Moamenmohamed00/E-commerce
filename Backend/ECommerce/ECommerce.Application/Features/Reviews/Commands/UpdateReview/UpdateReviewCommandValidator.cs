using FluentValidation;

namespace ECommerce.Application.Features.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Review Id must be greater than 0.");

        RuleFor(v => v.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(v => v.Comment)
            .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.");
    }
}
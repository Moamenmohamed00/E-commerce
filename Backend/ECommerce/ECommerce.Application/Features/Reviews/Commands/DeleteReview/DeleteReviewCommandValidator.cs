using FluentValidation;

namespace ECommerce.Application.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Review Id must be greater than 0.");
    }
}
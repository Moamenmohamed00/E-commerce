using FluentValidation;

namespace ECommerce.Application.Features.Admin.Users.Commands.UpdateUserStatus;

public class UpdateUserStatusCommandValidator : AbstractValidator<UpdateUserStatusCommand>
{
    public UpdateUserStatusCommandValidator()
    {
        RuleFor(v => v.UserId)
            .GreaterThan(0).WithMessage("User Id must be greater than 0.");
    }
}
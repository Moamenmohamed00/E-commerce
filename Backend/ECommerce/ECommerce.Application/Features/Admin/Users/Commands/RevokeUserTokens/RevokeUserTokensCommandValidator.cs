using FluentValidation;

namespace ECommerce.Application.Features.Admin.Users.Commands.RevokeUserTokens;

public class RevokeUserTokensCommandValidator : AbstractValidator<RevokeUserTokensCommand>
{
    public RevokeUserTokensCommandValidator()
    {
        RuleFor(v => v.UserId)
            .GreaterThan(0).WithMessage("User Id must be greater than 0.");
    }
}
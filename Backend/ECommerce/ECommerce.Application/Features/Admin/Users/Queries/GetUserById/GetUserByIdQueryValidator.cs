using FluentValidation;
namespace ECommerce.Application.Features.Admin.Users.Queries.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
    }
}
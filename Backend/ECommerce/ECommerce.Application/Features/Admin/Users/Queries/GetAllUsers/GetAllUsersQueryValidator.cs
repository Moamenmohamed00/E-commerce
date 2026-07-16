using FluentValidation;
namespace ECommerce.Application.Features.Admin.Users.Queries.GetAllUsers;
public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidator()
    {
        RuleFor(v => v.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(v => v.PageSize).InclusiveBetween(1, 100); 
    }
}
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using FluentValidation;

public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
    }
}
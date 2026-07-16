using FluentValidation;

namespace ECommerce.Application.Features.Payments.Queries.GetPaymentByOrderId;

public class GetPaymentByOrderIdQueryValidator : AbstractValidator<GetPaymentByOrderIdQuery>
{
    public GetPaymentByOrderIdQueryValidator()
    {
        RuleFor(v => v.OrderId)
            .GreaterThan(0).WithMessage("Order Id must be greater than 0.");
    }
}
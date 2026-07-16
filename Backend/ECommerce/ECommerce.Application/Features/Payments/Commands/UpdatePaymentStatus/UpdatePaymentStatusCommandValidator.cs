using FluentValidation;

namespace ECommerce.Application.Features.Payments.Commands.UpdatePaymentStatus;

public class UpdatePaymentStatusCommandValidator : AbstractValidator<UpdatePaymentStatusCommand>
{
    public UpdatePaymentStatusCommandValidator()
    {
        RuleFor(v => v.OrderId).GreaterThan(0);
        RuleFor(v => v.Status).NotEmpty();
        // الـ TransactionId قد يكون فارغاً في بعض بوابات الدفع عند الفشل، لذا لم نجعله NotEmpty
    }
}
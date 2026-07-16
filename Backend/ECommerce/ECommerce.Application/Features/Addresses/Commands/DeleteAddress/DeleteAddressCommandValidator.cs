using FluentValidation;

namespace ECommerce.Application.Features.Addresses.Commands.DeleteAddress
{

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0)
            .WithMessage("Address Id must be greater than 0.");
    }
}
}
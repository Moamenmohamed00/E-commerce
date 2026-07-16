using FluentValidation;

namespace ECommerce.Application.Features.Addresses.Commands.CreateAddress
{

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(v => v.Country).NotEmpty().MaximumLength(50);
        RuleFor(v => v.City).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Street).NotEmpty().MaximumLength(100);
        RuleFor(v => v.Building).NotEmpty().MaximumLength(50);
        RuleFor(v => v.PostalCode).MaximumLength(20);
    }
}
}
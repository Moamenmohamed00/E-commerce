using FluentValidation;

namespace ECommerce.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.Country).NotEmpty().MaximumLength(50);
        RuleFor(v => v.City).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Street).NotEmpty().MaximumLength(100);
        RuleFor(v => v.Building).NotEmpty().MaximumLength(50);
        RuleFor(v => v.PostalCode).MaximumLength(20);
    }
}
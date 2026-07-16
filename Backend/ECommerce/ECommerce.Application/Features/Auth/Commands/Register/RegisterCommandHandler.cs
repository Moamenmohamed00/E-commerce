using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(
        IIdentityService identityService, 
        IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // 1. محاولة إنشاء المستخدم (هذه الدالة ستستخدم UserManager في الـ Infrastructure)
        var result = await _identityService.CreateUserAsync(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password, 
            request.PhoneNumber);

        if (result.IsFailure)
        {
            return result;
        }

        var verificationToken = await _identityService.GenerateEmailConfirmationTokenAsync(request.Email);

        await _emailService.SendVerificationEmailAsync(request.Email, verificationToken, cancellationToken);

        return Result.Success();
    }
}
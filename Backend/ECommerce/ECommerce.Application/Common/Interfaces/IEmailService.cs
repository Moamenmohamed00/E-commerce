namespace ECommerce.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlMessage, CancellationToken cancellationToken);
        Task SendVerificationEmailAsync(string to, string token, CancellationToken cancellationToken);
        Task SendPasswordResetEmailAsync(string to, string resetToken, CancellationToken cancellationToken);
    }
}
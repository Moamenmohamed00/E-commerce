using ECommerce.Domain.Entities;
using System.Collections.Generic;
using ECommerce.Application.Common.Models;
namespace ECommerce.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(User user,IEnumerable<string> roles,CancellationToken cancellationToken=default);
        int? GetUserIdFromExpiredToken(string token);
        Task<string> GenerateRefreshTokenAsync(User user,CancellationToken cancellationToken=default);
        Task RevokeRefreshTokenAsync(string refreshToken,CancellationToken cancellationToken=default);
        // Task<string> GeneratePasswordResetTokenAsync(string email);
        // Task<Result> ResetPasswordAsync(string email, string token, string newPassword);
        // Task<Result> VerifyEmailAsync(string email, string token);
    }
}
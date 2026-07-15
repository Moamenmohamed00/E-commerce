using ECommerce.Domain.Entities;
using System.Collections.Generic;
namespace ECommerce.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(User user,IEnumerable<string> roles,CancellationToken cancellationToken=default);
        Task<string> GenerateRefreshTokenAsync(User user,CancellationToken cancellationToken=default);
        Task RevokeRefreshTokenAsync(string refreshToken,CancellationToken cancellationToken=default);
    }
}
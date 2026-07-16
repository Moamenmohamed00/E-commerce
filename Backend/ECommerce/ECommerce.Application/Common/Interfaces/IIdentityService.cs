using ECommerce.Application.Common.Models;

namespace ECommerce.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result> CreateUserAsync(string firstName, string lastName, string email, string password, string phoneNumber);
    Task<bool> CheckPasswordAsync(string email, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(string email);
}
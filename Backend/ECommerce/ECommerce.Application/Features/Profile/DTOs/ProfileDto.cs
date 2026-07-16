using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Profile.DTOs;

public record ProfileDto(string FirstName, string LastName, string Email, string PhoneNumber) : IMapFrom<User>;
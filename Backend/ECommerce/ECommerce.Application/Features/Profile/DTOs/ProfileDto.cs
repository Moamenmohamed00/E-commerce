using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Profile.DTOs;

public record ProfileDto(string FirstName, string LastName, string Email, string PhoneNumber) : IMapFrom<User>
{
    // Parameterless constructor required by Mapster's design-time reflection
    public ProfileDto() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
}
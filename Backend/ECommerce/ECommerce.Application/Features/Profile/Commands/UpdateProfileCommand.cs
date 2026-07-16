using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Profile.Commands.UpdateProfile;

public record UpdateProfileCommand(
    string FirstName, 
    string LastName, 
    string PhoneNumber) : IRequest<Result>;
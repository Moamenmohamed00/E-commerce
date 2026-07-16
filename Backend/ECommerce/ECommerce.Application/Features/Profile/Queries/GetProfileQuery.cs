using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Profile.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Profile.Queries.GetProfile;

public record GetProfileQuery() : IRequest<Result<ProfileDto>>;
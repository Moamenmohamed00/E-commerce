using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Addresses.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Addresses.Queries.GetUserAddresses;

public record GetUserAddressesQuery() : IRequest<Result<IEnumerable<AddressDto>>>;
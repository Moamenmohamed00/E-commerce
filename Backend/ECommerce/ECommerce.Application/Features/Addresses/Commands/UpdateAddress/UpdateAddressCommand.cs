using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Addresses.Commands.UpdateAddress;

public record UpdateAddressCommand(
    int Id,
    string Country, 
    string City, 
    string Street, 
    string Building, 
    string PostalCode, 
    bool IsDefault) : IRequest<Result>;
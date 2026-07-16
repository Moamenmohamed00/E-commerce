using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Addresses.Commands.CreateAddress
{

public record CreateAddressCommand(
    string Country, 
    string City, 
    string Street, 
    string Building, 
    string PostalCode, 
    bool IsDefault) : IRequest<Result<int>>;
}
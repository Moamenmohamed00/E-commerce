using ECommerce.Application.Common.Mappings;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Addresses.DTOs;

public record AddressDto(
    int Id, 
    string Country, 
    string City, 
    string Street, 
    string Building, 
    string PostalCode, 
    bool IsDefault) : IMapFrom<Address>;
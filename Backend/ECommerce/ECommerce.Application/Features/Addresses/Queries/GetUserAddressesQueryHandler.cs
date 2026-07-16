using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Addresses.DTOs;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Addresses.Queries.GetUserAddresses;

public class GetUserAddressesQueryHandler : IRequestHandler<GetUserAddressesQuery, Result<IEnumerable<AddressDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetUserAddressesQueryHandler(
        IApplicationDbContext context, 
        ICurrentUserService currentUserService, 
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<AddressDto>>> Handle(GetUserAddressesQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<IEnumerable<AddressDto>>.Failure("Unauthorized");

        var addresses = await _context.Addresses
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.IsDefault) 
            .ThenByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        var addressDtos = _mapper.Map<IEnumerable<AddressDto>>(addresses);
        
        return Result<IEnumerable<AddressDto>>.Success(addressDtos);
    }
}
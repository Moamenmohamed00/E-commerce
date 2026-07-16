using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Addresses.Commands.CreateAddress
{

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateAddressCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<int>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<int>.Failure("Unauthorized");

        bool isDefault = request.IsDefault;

        var hasAnyAddress = await _context.Addresses
            .AnyAsync(a => a.UserId == userId && !a.IsDeleted, cancellationToken);
            
        if (!hasAnyAddress)
        {
            isDefault = true;
        }

        if (isDefault && hasAnyAddress)
        {
            var existingDefaultAddresses = await _context.Addresses
                .Where(a => a.UserId == userId && a.IsDefault && !a.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var addr in existingDefaultAddresses)
            {
                addr.IsDefault = false;
                addr.UpdatedAt = DateTime.UtcNow; 
            }
        }

        var address = new Address
        {
            UserId = userId.Value,
            Country = request.Country,
            City = request.City,
            Street = request.Street,
            Building = request.Building,
            PostalCode = request.PostalCode,
            IsDefault = isDefault,
            CreatedAt = DateTime.UtcNow
        };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(address.Id);
    }
}
}
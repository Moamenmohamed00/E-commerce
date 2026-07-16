using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Admin.Users.Commands.UpdateUserStatus;

public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        var currentAdminId = _currentUserService.UserId;
        if (currentAdminId == null) return Result.Failure("Unauthorized");

        if (request.UserId == currentAdminId && !request.IsActive)
        {
            return Result.Failure("Critical: You cannot deactivate your own admin account.");
        }


        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Failure("User not found.");
        }

        var newStatus = request.IsActive ? UserStatus.Active : UserStatus.Suspended;

        if (user.UserStatus == newStatus)
        {
            return Result.Success(); 
        }

        user.UserStatus = newStatus;
         user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
using ECommerce.Application.Features.Admin.Users.Commands.RevokeUserTokens;
using ECommerce.Application.Features.Admin.Users.Commands.UpdateUserStatus;
using ECommerce.Application.Features.Admin.Users.Queries.GetAllUsers;
using ECommerce.Application.Features.Admin.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class AdminUsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await Mediator.Send(new GetUserByIdQuery(id));
        return HandleResult(result);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UpdateUserStatusCommand command)
    {
        if (id != command.UserId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("{id}/revoke-tokens")]
    public async Task<IActionResult> RevokeTokens(int id)
    {
        var result = await Mediator.Send(new RevokeUserTokensCommand(id));
        return HandleResult(result);
    }
}

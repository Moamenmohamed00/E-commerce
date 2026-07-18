using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Features.Profile.Queries.GetProfile;
using ECommerce.Application.Features.Profile.Commands.UpdateProfile;

namespace ECommerce.Api.Controllers;

[Authorize]
public class ProfileController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var result = await Mediator.Send(new GetProfileQuery());
        return HandleResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}

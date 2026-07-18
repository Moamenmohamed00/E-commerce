using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Features.Addresses.Commands.CreateAddress;
using ECommerce.Application.Features.Addresses.Commands.UpdateAddress;
using ECommerce.Application.Features.Addresses.Commands.DeleteAddress;
using ECommerce.Application.Features.Addresses.Queries.GetUserAddresses;

namespace ECommerce.Api.Controllers;

[Authorize]
public class AddressesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserAddresses()
    {
        var result = await Mediator.Send(new GetUserAddressesQuery());
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateAddressCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { message = "Id in route does not match Id in body." });
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var result = await Mediator.Send(new DeleteAddressCommand(id));
        return HandleResult(result);
    }
}

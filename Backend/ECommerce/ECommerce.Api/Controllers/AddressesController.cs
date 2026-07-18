using ECommerce.Application.Features.Addresses.Commands.CreateAddress;
using ECommerce.Application.Features.Addresses.Commands.DeleteAddress;
using ECommerce.Application.Features.Addresses.Commands.UpdateAddress;
using ECommerce.Application.Features.Addresses.Queries.GetUserAddresses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AddressesController(ISender sender)
    : ApiControllerBase(sender)
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressCommand command)
        {
            var result = await Sender.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Sender.Send(new GetUserAddressesQuery());
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateAddressCommand command)
        {
            var result = await Sender.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await Sender.Send(new DeleteAddressCommand(id));

            return success.IsSuccess
                ? NoContent()
                : NotFound();
        }
    }
}

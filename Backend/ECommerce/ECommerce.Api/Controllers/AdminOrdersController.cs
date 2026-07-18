using ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/admin/orders")]
[Authorize(Roles = "Admin")]
public class AdminOrdersController : ApiControllerBase
{
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusCommand command)
    {
        if (id != command.OrderId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}

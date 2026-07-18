using ECommerce.Application.Features.ProductVariants.Commands.UpdateStock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/admin/inventory")]
[Authorize(Roles = "Admin")]
public class InventoryController : ApiControllerBase
{
    [HttpPut("stock")]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateStockCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}

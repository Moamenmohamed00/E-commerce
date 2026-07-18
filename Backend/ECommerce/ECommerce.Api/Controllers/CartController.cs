using ECommerce.Application.Features.Cart.Commands.AddToCart;
using ECommerce.Application.Features.Cart.Commands.ClearCart;
using ECommerce.Application.Features.Cart.Commands.RemoveFromCart;
using ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;
using ECommerce.Application.Features.Cart.Queries.GetCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Authorize]
public class CartController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var result = await Mediator.Send(new GetCartQuery());
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuantity(int id, [FromBody] UpdateCartItemQuantityCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveItem(int id)
    {
        var result = await Mediator.Send(new RemoveFromCartCommand(id));
        return HandleResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart()
    {
        var result = await Mediator.Send(new ClearCartCommand());
        return HandleResult(result);
    }
}

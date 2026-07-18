using ECommerce.Application.Features.Wishlist.Commands.AddToWishlist;
using ECommerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;
using ECommerce.Application.Features.Wishlist.Queries.GetWishlist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Authorize]
public class WishlistController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetWishlist()
    {
        var result = await Mediator.Send(new GetWishlistQuery());
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist([FromBody] AddToWishlistCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromWishlist(int id)
    {
        var result = await Mediator.Send(new RemoveFromWishlistCommand(id));
        return HandleResult(result);
    }
}

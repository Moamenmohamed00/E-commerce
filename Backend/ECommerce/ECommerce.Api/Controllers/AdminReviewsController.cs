using ECommerce.Application.Features.Reviews.Commands.DeleteReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/admin/reviews")]
[Authorize(Roles = "Admin")]
public class AdminReviewsController : ApiControllerBase
{
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var result = await Mediator.Send(new DeleteReviewCommand(id));
        return HandleResult(result);
    }
}

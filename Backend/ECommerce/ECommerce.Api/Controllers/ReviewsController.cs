using ECommerce.Application.Features.Reviews.Commands.CreateReview;
using ECommerce.Application.Features.Reviews.Commands.DeleteReview;
using ECommerce.Application.Features.Reviews.Commands.UpdateReview;
using ECommerce.Application.Features.Reviews.Queries.GetReviewsByProductId;
using ECommerce.Application.Features.Reviews.Queries.GetUserReviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/reviews")]
public class ReviewsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetProductReviews(int productId)
    {
        var result = await Mediator.Send(new GetReviewsByProductIdQuery(productId));
        return HandleResult(result);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUserReviews()
    {
        var result = await Mediator.Send(new GetUserReviewsQuery());
        return HandleResult(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var result = await Mediator.Send(new DeleteReviewCommand(id));
        return HandleResult(result);
    }
}

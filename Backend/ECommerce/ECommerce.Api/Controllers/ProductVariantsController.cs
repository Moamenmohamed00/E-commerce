using ECommerce.Application.Features.ProductVariants.Commands.CreateProductVariant;
using ECommerce.Application.Features.ProductVariants.Commands.DeleteProductVariant;
using ECommerce.Application.Features.ProductVariants.Commands.UpdateProductVariant;
using ECommerce.Application.Features.ProductVariants.Queries.GetVariantsByProductId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/products/{productId}/variants")]
public class ProductVariantsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetVariants(int productId)
    {
        var result = await Mediator.Send(new GetVariantsByProductIdQuery(productId));
        return HandleResult(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateVariant(int productId, [FromBody] CreateProductVariantCommand command)
    {
        if (productId != command.ProductId)
        {
            return BadRequest("Product ID mismatch");
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateVariant(int productId, int id, [FromBody] UpdateProductVariantCommand command)
    {
        if (productId != command.ProductId || id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteVariant(int productId, int id)
    {
        var result = await Mediator.Send(new DeleteProductVariantCommand(id, productId));
        return HandleResult(result);
    }
}

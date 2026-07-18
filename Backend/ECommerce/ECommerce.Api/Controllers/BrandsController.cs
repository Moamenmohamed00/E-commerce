using ECommerce.Application.Features.Brands.Commands.CreateBrand;
using ECommerce.Application.Features.Brands.Commands.DeleteBrand;
using ECommerce.Application.Features.Brands.Commands.UpdateBrand;
using ECommerce.Application.Features.Brands.Queries.GetBrandById;
using ECommerce.Application.Features.Brands.Queries.GetBrands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

public class BrandsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
        var result = await Mediator.Send(new GetBrandsQuery());
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
        var result = await Mediator.Send(new GetBrandByIdQuery(id));
        return HandleResult(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] UpdateBrandCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var result = await Mediator.Send(new DeleteBrandCommand(id));
        return HandleResult(result);
    }
}

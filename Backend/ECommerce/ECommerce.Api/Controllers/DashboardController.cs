using ECommerce.Application.Features.Admin.Dashboard.Queries.GetDashboardSummary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/admin/dashboard")]
[Authorize(Roles = "Admin")]
public class DashboardController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDashboardSummary()
    {
        var result = await Mediator.Send(new GetDashboardSummaryQuery());
        return HandleResult(result);
    }
}

using ECommerce.Application.Features.Payments.Commands.CreatePaymentIntent;
using ECommerce.Application.Features.Payments.Commands.UpdatePaymentStatus;
using ECommerce.Application.Features.Payments.Queries.GetPaymentByOrderId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ECommerce.Api.Controllers;

[Authorize]
public class PaymentsController : ApiControllerBase
{
    private readonly IConfiguration _configuration;

    public PaymentsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetPaymentByOrderId(int orderId)
    {
        var result = await Mediator.Send(new GetPaymentByOrderIdQuery(orderId));
        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var endpointSecret = _configuration["Stripe:WebhookSecret"];

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json, 
                Request.Headers["Stripe-Signature"], 
                endpointSecret
            );

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                if (paymentIntent != null && paymentIntent.Metadata.TryGetValue("OrderId", out string? orderIdStr) && int.TryParse(orderIdStr, out int orderId))
                {
                    await Mediator.Send(new UpdatePaymentStatusCommand(orderId, paymentIntent.Id, "Paid"));
                }
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                if (paymentIntent != null && paymentIntent.Metadata.TryGetValue("OrderId", out string? orderIdStr) && int.TryParse(orderIdStr, out int orderId))
                {
                    await Mediator.Send(new UpdatePaymentStatusCommand(orderId, paymentIntent.Id, "Failed"));
                }
            }

            return Ok();
        }
        catch (StripeException)
        {
            return BadRequest();
        }
    }
}

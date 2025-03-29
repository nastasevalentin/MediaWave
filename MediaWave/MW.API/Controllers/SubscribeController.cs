using Microsoft.AspNetCore.Mvc;
using MW.Application.Contracts.Interfaces;
using MW.Application.Models.Integration;

namespace MW.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribeController : ApiControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscribeController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid payload");
        }

        var result = await _subscriptionService.SubscribeAsync(request.UserId, request.ChannelId);

        if (!result)
        {
            return BadRequest("Subscription failed");
        }

        return Ok("Subscribed successfully");
    }

    [HttpPost("unsubscribe")]
    public async Task<IActionResult> Unsubscribe([FromBody] UnsubscribeRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid payload");
        }

        var result = await _subscriptionService.UnsubscribeAsync(request.UserId, request.ChannelId);

        if (!result)
        {
            return BadRequest("Unsubscription failed");
        }

        return Ok("Unsubscribed successfully");
    }
}
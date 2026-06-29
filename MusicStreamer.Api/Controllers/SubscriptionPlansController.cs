using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionPlansController(IServicoPlanosAssinatura subscriptionService) : ControllerBase
{
    [HttpGet("plans")]
    public async Task<ActionResult<IReadOnlyList<PlanoAssinaturaDto>>> GetPlans(CancellationToken cancellationToken)
    {
        return Ok(await subscriptionService.GetPlansAsync(cancellationToken));
    }

    [Authorize]
    [HttpPost("choose-plan")]
    public async Task<ActionResult<AssinaturaUsuarioDto>> ChoosePlan([FromBody] ChoosePlanRequest request, CancellationToken cancellationToken)
    {
        var response = await subscriptionService.ChoosePlanAsync(new EscolherPlanoDto(request.UserId, request.PlanId), cancellationToken);
        return response is null ? NotFound() : Ok(response);
    }
}


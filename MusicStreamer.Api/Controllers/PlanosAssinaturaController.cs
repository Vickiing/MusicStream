using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Route("api/subscriptions")]
public sealed class PlanosAssinaturaController(IServicoPlanosAssinatura servicoPlanosAssinatura) : ControllerBase
{
    [HttpGet("plans")]
    public async Task<ActionResult<IReadOnlyList<PlanoAssinaturaDto>>> GetPlans(CancellationToken cancellationToken)
    {
        return Ok(await servicoPlanosAssinatura.GetPlansAsync(cancellationToken));
    }

    [Authorize]
    [HttpPost("choose-plan")]
    public async Task<ActionResult<AssinaturaUsuarioDto>> ChoosePlan([FromBody] EscolherPlanoRequest request, CancellationToken cancellationToken)
    {
        var response = await servicoPlanosAssinatura.ChoosePlanAsync(new EscolherPlanoDto(request.UserId, request.PlanId), cancellationToken);
        return response is null ? NotFound() : Ok(response);
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/transactions")]
public sealed class TransactionsController(IServicoTransacoes transactionService) : ControllerBase
{
    [HttpPost("authorize")]
    public async Task<ActionResult<ResultadoTransacaoDto>> AuthorizeTransaction([FromBody] AuthorizeTransactionRequest request, CancellationToken cancellationToken)
    {
        var input = new AutorizarTransacaoDto(
            request.UserId,
            request.MerchantId,
            request.Amount,
            request.RequestedAtUtc ?? DateTimeOffset.UtcNow);

        var result = await transactionService.AuthorizeAsync(input, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ResultadoTransacaoDto>>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await transactionService.GetByUserAsync(userId, cancellationToken));
    }
}


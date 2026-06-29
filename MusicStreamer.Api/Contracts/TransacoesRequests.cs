using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record AutorizarTransacaoRequest(
    [property: Required] Guid UserId,
    [property: Required] Guid MerchantId,
    [property: Range(0.01, 1000000)] decimal Amount,
    DateTimeOffset? RequestedAtUtc);

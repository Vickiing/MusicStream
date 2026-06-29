using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record EscolherPlanoRequest([property: Required] Guid UserId, [property: Required] Guid PlanId);

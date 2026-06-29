using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record ChoosePlanRequest([property: Required] Guid UserId, [property: Required] Guid PlanId);

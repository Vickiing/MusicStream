using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface ISubscriptionService
{
    Task<IReadOnlyList<SubscriptionPlanDto>> GetPlansAsync(CancellationToken cancellationToken = default);
    Task<UserSubscriptionDto?> ChoosePlanAsync(ChoosePlanDto input, CancellationToken cancellationToken = default);
}

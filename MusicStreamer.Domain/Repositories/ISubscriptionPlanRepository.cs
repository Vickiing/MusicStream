using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface ISubscriptionPlanRepository
{
    Task<IReadOnlyList<SubscriptionPlan>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SubscriptionPlan?> GetByIdAsync(Guid planId, CancellationToken cancellationToken = default);
}

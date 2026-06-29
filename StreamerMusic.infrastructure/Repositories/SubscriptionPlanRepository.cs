using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class SubscriptionPlanRepository(MusicStreamerDbContext dbContext) : ISubscriptionPlanRepository
{
    public async Task<IReadOnlyList<SubscriptionPlan>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SubscriptionPlans
            .AsNoTracking()
            .Where(item => item.IsActive)
            .OrderBy(item => item.MonthlyPrice)
            .ToListAsync(cancellationToken);
    }

    public Task<SubscriptionPlan?> GetByIdAsync(Guid planId, CancellationToken cancellationToken = default)
    {
        return dbContext.SubscriptionPlans.FirstOrDefaultAsync(item => item.Id == planId, cancellationToken);
    }
}

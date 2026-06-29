using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class PlanoAssinaturaRepository(MusicStreamerDbContext dbContext) : IPlanoAssinaturaRepository
{
    public async Task<IReadOnlyList<PlanoAssinatura>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SubscriptionPlans
            .AsNoTracking()
            .Where(item => item.IsActive)
            .OrderBy(item => item.MonthlyPrice)
            .ToListAsync(cancellationToken);
    }

    public Task<PlanoAssinatura?> GetByIdAsync(Guid planId, CancellationToken cancellationToken = default)
    {
        return dbContext.SubscriptionPlans.FirstOrDefaultAsync(item => item.Id == planId, cancellationToken);
    }
}



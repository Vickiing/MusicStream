using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class MerchantRepository(MusicStreamerDbContext dbContext) : IComercianteRepository
{
    public Task<Comerciante?> GetByIdAsync(Guid merchantId, CancellationToken cancellationToken = default)
    {
        return dbContext.Merchants.FirstOrDefaultAsync(item => item.Id == merchantId, cancellationToken);
    }

    public async Task<IReadOnlyList<Comerciante>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Merchants
            .AsNoTracking()
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);
    }
}


using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Enums;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class TransacaoRepository(MusicStreamerDbContext dbContext) : ITransacaoRepository
{
    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        dbContext.Transactions.Add(transaction);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Transaction?> GetLastApprovedTransactionAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return dbContext.Transactions
            .AsNoTracking()
            .Where(item => item.UserAccountId == userId && item.Status == StatusTransacao.Approved)
            .OrderByDescending(item => item.RequestedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Transaction>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Transactions
            .AsNoTracking()
            .Include(item => item.Notifications)
            .Where(item => item.UserAccountId == userId)
            .OrderByDescending(item => item.RequestedAtUtc)
            .ToListAsync(cancellationToken);
    }
}



using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task<Transaction?> GetLastApprovedTransactionAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Transaction>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}

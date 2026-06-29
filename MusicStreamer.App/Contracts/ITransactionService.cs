using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface ITransactionService
{
    Task<TransactionResultDto?> AuthorizeAsync(AuthorizeTransactionDto input, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TransactionResultDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}

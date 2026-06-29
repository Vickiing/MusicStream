using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IMerchantRepository
{
    Task<Merchant?> GetByIdAsync(Guid merchantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Merchant>> GetAllAsync(CancellationToken cancellationToken = default);
}

using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IComercianteRepository
{
    Task<Comerciante?> GetByIdAsync(Guid merchantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Comerciante>> GetAllAsync(CancellationToken cancellationToken = default);
}


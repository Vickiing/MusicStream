using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IPlanoAssinaturaRepository
{
    Task<IReadOnlyList<PlanoAssinatura>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlanoAssinatura?> GetByIdAsync(Guid planId, CancellationToken cancellationToken = default);
}


using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IContaUsuarioRepository
{
    Task AddAsync(ContaUsuario account, CancellationToken cancellationToken = default);
    Task<ContaUsuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<ContaUsuario?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(ContaUsuario account, CancellationToken cancellationToken = default);
}


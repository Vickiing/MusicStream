using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IUserAccountRepository
{
    Task AddAsync(UserAccount account, CancellationToken cancellationToken = default);
    Task<UserAccount?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<UserAccount?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserAccount account, CancellationToken cancellationToken = default);
}

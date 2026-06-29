using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class UserAccountRepository(MusicStreamerDbContext dbContext) : IUserAccountRepository
{
    public async Task AddAsync(UserAccount account, CancellationToken cancellationToken = default)
    {
        dbContext.UserAccounts.Add(account);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<UserAccount?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToUpperInvariant();
        return dbContext.UserAccounts.FirstOrDefaultAsync(item => item.Email.NormalizedValue == normalizedEmail, cancellationToken);
    }

    public Task<UserAccount?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return dbContext.UserAccounts.FirstOrDefaultAsync(item => item.Id == userId, cancellationToken);
    }

    public async Task UpdateAsync(UserAccount account, CancellationToken cancellationToken = default)
    {
        dbContext.UserAccounts.Update(account);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

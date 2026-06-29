using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class PlaylistRepository(MusicStreamerDbContext dbContext) : IPlaylistRepository
{
    public async Task AddAsync(Playlist playlist, CancellationToken cancellationToken = default)
    {
        dbContext.Playlists.Add(playlist);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Playlist?> GetByIdAsync(Guid playlistId, CancellationToken cancellationToken = default)
    {
        return dbContext.Playlists
            .Include(item => item.Tracks)
                .ThenInclude(item => item.Music)
                    .ThenInclude(item => item!.Artist)
            .Include(item => item.Tracks)
                .ThenInclude(item => item.Music)
                    .ThenInclude(item => item!.Album)
            .FirstOrDefaultAsync(item => item.Id == playlistId, cancellationToken);
    }

    public async Task<IReadOnlyList<Playlist>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Playlists
            .AsNoTracking()
            .Include(item => item.Tracks)
                .ThenInclude(item => item.Music)
                    .ThenInclude(item => item!.Artist)
            .Include(item => item.Tracks)
                .ThenInclude(item => item.Music)
                    .ThenInclude(item => item!.Album)
            .Where(item => item.UserAccountId == userId)
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Playlist playlist, CancellationToken cancellationToken = default)
    {
        dbContext.Playlists.Update(playlist);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

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
                    .ThenInclude(item => item!.Banda)
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
                    .ThenInclude(item => item!.Banda)
            .Include(item => item.Tracks)
                .ThenInclude(item => item.Music)
                    .ThenInclude(item => item!.Album)
            .Where(item => item.UserAccountId == userId)
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AddTrackAsync(Guid playlistId, Guid trackId, CancellationToken cancellationToken = default)
    {
        try
        {
            var alreadyExists = await dbContext.PlaylistTracks
                .AnyAsync(item => item.PlaylistId == playlistId && item.MusicId == trackId, cancellationToken);

            if (alreadyExists)
            {
                return true;
            }

            dbContext.PlaylistTracks.Add(FaixaPlaylist.Create(playlistId, trackId));

            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }
}


using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class CatalogRepository(MusicStreamerDbContext dbContext) : ICatalogRepository
{
    public async Task<IReadOnlyList<Artist>> GetArtistsAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Artists
            .AsNoTracking()
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Album>> GetAlbumsAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Albums
            .AsNoTracking()
            .Include(item => item.Artist)
            .OrderBy(item => item.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MusicTrack>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.MusicTracks
            .AsNoTracking()
            .Include(item => item.Artist)
            .Include(item => item.Album)
            .OrderBy(item => item.Title)
            .ToListAsync(cancellationToken);
    }

    public Task<Artist?> GetArtistByIdAsync(Guid artistId, CancellationToken cancellationToken = default)
    {
        return dbContext.Artists.FirstOrDefaultAsync(item => item.Id == artistId, cancellationToken);
    }

    public Task<MusicTrack?> GetTrackByIdAsync(Guid trackId, CancellationToken cancellationToken = default)
    {
        return dbContext.MusicTracks
            .Include(item => item.Artist)
            .Include(item => item.Album)
            .FirstOrDefaultAsync(item => item.Id == trackId, cancellationToken);
    }

    public async Task<CatalogSearchResult> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        var normalizedTerm = term.Trim().ToUpperInvariant();

        var artists = await dbContext.Artists
            .AsNoTracking()
            .Where(item => item.NormalizedName.StartsWith(normalizedTerm))
            .OrderBy(item => item.Name)
            .Take(10)
            .Select(item => new CatalogSearchArtist(item.Id, item.Name))
            .ToListAsync(cancellationToken);

        var tracks = await dbContext.MusicTracks
            .AsNoTracking()
            .Include(item => item.Artist)
            .Include(item => item.Album)
            .Where(item => item.NormalizedTitle.StartsWith(normalizedTerm) || item.Artist.NormalizedName.StartsWith(normalizedTerm))
            .OrderBy(item => item.Title)
            .Take(10)
            .Select(item => new CatalogSearchTrack(item.Id, item.Title, item.Artist.Name, item.Album.Title, item.DurationSeconds))
            .ToListAsync(cancellationToken);

        return new CatalogSearchResult(artists, tracks);
    }
}

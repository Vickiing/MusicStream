using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class CatalogoRepository(MusicStreamerDbContext dbContext) : ICatalogoRepository
{
    public async Task<IReadOnlyList<Banda>> GetArtistsAsync(CancellationToken cancellationToken = default)
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
            .Include(item => item.Banda)
            .OrderBy(item => item.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Musica>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.MusicTracks
            .AsNoTracking()
            .Include(item => item.Banda)
            .Include(item => item.Album)
            .OrderBy(item => item.Title)
            .ToListAsync(cancellationToken);
    }

    public Task<Banda?> GetArtistByIdAsync(Guid artistId, CancellationToken cancellationToken = default)
    {
        return dbContext.Artists.FirstOrDefaultAsync(item => item.Id == artistId, cancellationToken);
    }

    public Task<Musica?> GetTrackByIdAsync(Guid trackId, CancellationToken cancellationToken = default)
    {
        return dbContext.MusicTracks
            .Include(item => item.Banda)
            .Include(item => item.Album)
            .FirstOrDefaultAsync(item => item.Id == trackId, cancellationToken);
    }

    public async Task<ResultadoBuscaCatalogo> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        var normalizedTerm = term.Trim().ToUpperInvariant();

        var artists = await dbContext.Artists
            .AsNoTracking()
            .Where(item => item.NormalizedName.StartsWith(normalizedTerm))
            .OrderBy(item => item.Name)
            .Take(10)
            .Select(item => new BandaBuscaCatalogo(item.Id, item.Name))
            .ToListAsync(cancellationToken);

        var tracks = await dbContext.MusicTracks
            .AsNoTracking()
            .Include(item => item.Banda)
            .Include(item => item.Album)
            .Where(item => item.NormalizedTitle.StartsWith(normalizedTerm) || item.Banda.NormalizedName.StartsWith(normalizedTerm))
            .OrderBy(item => item.Title)
            .Take(10)
            .Select(item => new MusicaBuscaCatalogo(item.Id, item.Title, item.Banda.Name, item.Album.Title, item.DurationSeconds))
            .ToListAsync(cancellationToken);

        return new ResultadoBuscaCatalogo(artists, tracks);
    }
}



using Microsoft.EntityFrameworkCore;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Entities;
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

    public async Task<ResultadoBuscaCatalogoDto> SearchAsync(string term, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var normalizedTerm = term.Trim().ToUpperInvariant();

        var artists = await dbContext.Artists
            .AsNoTracking()
            .Where(item => item.NormalizedName.StartsWith(normalizedTerm))
            .OrderBy(item => item.Name)
            .Take(6)
            .Select(item => new BandaDto(item.Id, item.Name))
            .ToListAsync(cancellationToken);

        var tracksQuery = dbContext.MusicTracks
            .AsNoTracking()
            .Include(item => item.Banda)
            .Include(item => item.Album)
            .Where(item => item.NormalizedTitle.StartsWith(normalizedTerm)
                || item.Banda.NormalizedName.StartsWith(normalizedTerm)
                || item.Album.NormalizedTitle.StartsWith(normalizedTerm));

        var totalTracks = await tracksQuery.CountAsync(cancellationToken);
        var sanitizedPageSize = pageSize <= 0 ? 10 : pageSize;
        var sanitizedPage = page <= 0 ? 1 : page;

        var tracks = await tracksQuery
            .OrderBy(item => item.Title)
            .Skip((sanitizedPage - 1) * sanitizedPageSize)
            .Take(sanitizedPageSize)
            .Select(item => new MusicaDto(item.Id, item.Title, item.Banda.Name, item.Album.Title, item.DurationSeconds))
            .ToListAsync(cancellationToken);

        var totalPages = totalTracks == 0 ? 0 : (int)Math.Ceiling(totalTracks / (double)sanitizedPageSize);
        return new ResultadoBuscaCatalogoDto(artists, tracks, sanitizedPage, sanitizedPageSize, totalTracks, totalPages);
    }
}



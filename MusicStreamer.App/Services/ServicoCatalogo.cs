using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class ServicoCatalogo(ICatalogoRepository catalogRepository) : IServicoCatalogo
{
    public async Task<IReadOnlyList<BandaDto>> GetArtistsAsync(CancellationToken cancellationToken = default)
    {
        var artists = await catalogRepository.GetArtistsAsync(cancellationToken);
        return artists.Select(artist => new BandaDto(artist.Id, artist.Name)).ToList();
    }

    public async Task<IReadOnlyList<AlbumDto>> GetAlbumsAsync(CancellationToken cancellationToken = default)
    {
        var albums = await catalogRepository.GetAlbumsAsync(cancellationToken);
        return albums.Select(album => new AlbumDto(album.Id, album.Title, album.Banda.Name, album.ReleaseYear)).ToList();
    }

    public async Task<IReadOnlyList<MusicaDto>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        var tracks = await catalogRepository.GetTracksAsync(cancellationToken);
        return tracks
            .Select(track => new MusicaDto(track.Id, track.Title, track.Banda.Name, track.Album.Title, track.DurationSeconds))
            .ToList();
    }

    public Task<ResultadoBuscaCatalogoDto> SearchAsync(string term, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return SearchInternalAsync(term, page, pageSize, cancellationToken);
    }

    private async Task<ResultadoBuscaCatalogoDto> SearchInternalAsync(string term, int page, int pageSize, CancellationToken cancellationToken)
    {
        var result = await catalogRepository.SearchAsync(term, page, pageSize, cancellationToken);
        var totalPages = result.TotalTracks == 0 ? 0 : (int)Math.Ceiling(result.TotalTracks / (double)result.PageSize);
        return new ResultadoBuscaCatalogoDto(
            result.Artists.Select(item => new BandaDto(item.Id, item.Name)).ToList(),
            result.Tracks.Select(item => new MusicaDto(item.Id, item.Title, item.ArtistName, item.AlbumTitle, item.DurationSeconds)).ToList(),
            result.Page,
            result.PageSize,
            result.TotalTracks,
            totalPages);
    }
}



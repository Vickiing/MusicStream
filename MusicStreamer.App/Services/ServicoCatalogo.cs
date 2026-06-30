using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

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
        return catalogRepository.SearchAsync(term, page, pageSize, cancellationToken);
    }
}



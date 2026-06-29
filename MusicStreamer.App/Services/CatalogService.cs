using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class CatalogService(ICatalogRepository catalogRepository) : ICatalogService
{
    public async Task<IReadOnlyList<ArtistDto>> GetArtistsAsync(CancellationToken cancellationToken = default)
    {
        var artists = await catalogRepository.GetArtistsAsync(cancellationToken);
        return artists.Select(artist => new ArtistDto(artist.Id, artist.Name)).ToList();
    }

    public async Task<IReadOnlyList<AlbumDto>> GetAlbumsAsync(CancellationToken cancellationToken = default)
    {
        var albums = await catalogRepository.GetAlbumsAsync(cancellationToken);
        return albums.Select(album => new AlbumDto(album.Id, album.Title, album.Artist.Name, album.ReleaseYear)).ToList();
    }

    public async Task<IReadOnlyList<TrackDto>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        var tracks = await catalogRepository.GetTracksAsync(cancellationToken);
        return tracks
            .Select(track => new TrackDto(track.Id, track.Title, track.Artist.Name, track.Album.Title, track.DurationSeconds))
            .ToList();
    }

    public Task<CatalogSearchResultDto> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        return SearchInternalAsync(term, cancellationToken);
    }

    private async Task<CatalogSearchResultDto> SearchInternalAsync(string term, CancellationToken cancellationToken)
    {
        var result = await catalogRepository.SearchAsync(term, cancellationToken);
        return new CatalogSearchResultDto(
            result.Artists.Select(item => new ArtistDto(item.Id, item.Name)).ToList(),
            result.Tracks.Select(item => new TrackDto(item.Id, item.Title, item.ArtistName, item.AlbumTitle, item.DurationSeconds)).ToList());
    }
}

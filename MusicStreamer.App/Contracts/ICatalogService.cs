using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface ICatalogService
{
    Task<IReadOnlyList<ArtistDto>> GetArtistsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AlbumDto>> GetAlbumsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TrackDto>> GetTracksAsync(CancellationToken cancellationToken = default);
    Task<CatalogSearchResultDto> SearchAsync(string term, CancellationToken cancellationToken = default);
}

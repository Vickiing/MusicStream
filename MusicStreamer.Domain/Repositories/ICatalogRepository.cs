using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface ICatalogRepository
{
    Task<IReadOnlyList<Artist>> GetArtistsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Album>> GetAlbumsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MusicTrack>> GetTracksAsync(CancellationToken cancellationToken = default);
    Task<Artist?> GetArtistByIdAsync(Guid artistId, CancellationToken cancellationToken = default);
    Task<MusicTrack?> GetTrackByIdAsync(Guid trackId, CancellationToken cancellationToken = default);
    Task<CatalogSearchResult> SearchAsync(string term, CancellationToken cancellationToken = default);
}

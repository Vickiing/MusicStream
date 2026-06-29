using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IFavoritesService
{
    Task<FavoriteSummaryDto> FavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default);
    Task<FavoriteSummaryDto> UnfavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default);
    Task<FavoriteSummaryDto> FavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default);
    Task<FavoriteSummaryDto> UnfavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default);
}

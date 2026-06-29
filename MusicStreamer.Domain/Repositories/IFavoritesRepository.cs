using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IFavoritesRepository
{
    Task AddFavoriteTrackAsync(FavoriteMusic favoriteMusic, CancellationToken cancellationToken = default);
    Task RemoveFavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default);
    Task AddFavoriteArtistAsync(FavoriteBand favoriteBand, CancellationToken cancellationToken = default);
    Task RemoveFavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<FavoriteMusic> FavoriteTracks, IReadOnlyList<FavoriteBand> FavoriteBands)> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}

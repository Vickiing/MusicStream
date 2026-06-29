using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IRepositorioFavoritos
{
    Task AddFavoriteTrackAsync(MusicaFavorita favoriteMusic, CancellationToken cancellationToken = default);
    Task RemoveFavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default);
    Task AddFavoriteArtistAsync(BandaFavorita favoriteBand, CancellationToken cancellationToken = default);
    Task RemoveFavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<MusicaFavorita> FavoriteTracks, IReadOnlyList<BandaFavorita> FavoriteBands)> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}


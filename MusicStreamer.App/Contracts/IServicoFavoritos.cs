using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IServicoFavoritos
{
    Task<ResumoFavoritosDto> FavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default);
    Task<ResumoFavoritosDto> UnfavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default);
    Task<ResumoFavoritosDto> FavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default);
    Task<ResumoFavoritosDto> UnfavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default);
    Task<ResumoFavoritosDto> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}


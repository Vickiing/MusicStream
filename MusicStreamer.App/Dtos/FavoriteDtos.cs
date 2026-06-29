namespace MusicStreamer.App.DTOs;

public sealed record ResumoFavoritosDto(Guid UserId, IReadOnlyList<Guid> FavoriteTrackIds, IReadOnlyList<Guid> FavoriteArtistIds);


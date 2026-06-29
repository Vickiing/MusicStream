namespace MusicStreamer.App.DTOs;

public sealed record FavoriteSummaryDto(Guid UserId, IReadOnlyList<Guid> FavoriteTrackIds, IReadOnlyList<Guid> FavoriteArtistIds);

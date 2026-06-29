using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record FavoriteTrackRequest([property: Required] Guid UserId, [property: Required] Guid TrackId);

public sealed record FavoriteArtistRequest([property: Required] Guid UserId, [property: Required] Guid ArtistId);

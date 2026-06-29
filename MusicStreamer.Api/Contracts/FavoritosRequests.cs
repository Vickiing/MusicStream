using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record FavoritarMusicaRequest([property: Required] Guid UserId, [property: Required] Guid TrackId);

public sealed record FavoritarBandaRequest([property: Required] Guid UserId, [property: Required] Guid ArtistId);

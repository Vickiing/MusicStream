using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record CreatePlaylistRequest([property: Required] Guid UserId, [property: Required, StringLength(120)] string Name);

public sealed record AddTrackToPlaylistRequest([property: Required] Guid TrackId);

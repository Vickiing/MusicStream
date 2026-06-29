using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record CriarPlaylistRequest([property: Required] Guid UserId, [property: Required, StringLength(120)] string Name);

public sealed record AdicionarMusicaNaPlaylistRequest([property: Required] Guid TrackId);

namespace MusicStreamer.App.DTOs;

public sealed record CriarPlaylistDto(Guid UserId, string Name);

public sealed record AdicionarMusicaNaPlaylistDto(Guid PlaylistId, Guid TrackId);

public sealed record FaixaPlaylistDto(Guid TrackId, string Title, string ArtistName, string AlbumTitle);

public sealed record PlaylistDto(Guid Id, Guid UserId, string Name, IReadOnlyList<FaixaPlaylistDto> Tracks);


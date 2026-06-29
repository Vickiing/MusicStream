namespace MusicStreamer.App.DTOs;

public sealed record CreatePlaylistDto(Guid UserId, string Name);

public sealed record AddTrackToPlaylistDto(Guid PlaylistId, Guid TrackId);

public sealed record PlaylistTrackDto(Guid TrackId, string Title, string ArtistName, string AlbumTitle);

public sealed record PlaylistDto(Guid Id, Guid UserId, string Name, IReadOnlyList<PlaylistTrackDto> Tracks);

namespace MusicStreamer.App.DTOs;

public sealed record ArtistDto(Guid Id, string Name);

public sealed record AlbumDto(Guid Id, string Title, string ArtistName, int ReleaseYear);

public sealed record TrackDto(Guid Id, string Title, string ArtistName, string AlbumTitle, int DurationSeconds);

public sealed record CatalogSearchResultDto(IReadOnlyList<ArtistDto> Artists, IReadOnlyList<TrackDto> Tracks);

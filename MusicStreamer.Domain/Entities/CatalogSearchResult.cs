namespace MusicStreamer.Domain.Entities;

public sealed record CatalogSearchArtist(Guid Id, string Name);

public sealed record CatalogSearchTrack(Guid Id, string Title, string ArtistName, string AlbumTitle, int DurationSeconds);

public sealed record CatalogSearchResult(IReadOnlyList<CatalogSearchArtist> Artists, IReadOnlyList<CatalogSearchTrack> Tracks);

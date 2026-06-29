namespace MusicStreamer.Domain.Entities;

public sealed record BandaBuscaCatalogo(Guid Id, string Name);

public sealed record MusicaBuscaCatalogo(Guid Id, string Title, string ArtistName, string AlbumTitle, int DurationSeconds);

public sealed record ResultadoBuscaCatalogo(
    IReadOnlyList<BandaBuscaCatalogo> Artists,
    IReadOnlyList<MusicaBuscaCatalogo> Tracks,
    int Page,
    int PageSize,
    int TotalTracks);


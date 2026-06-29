namespace MusicStreamer.App.DTOs;

public sealed record BandaDto(Guid Id, string Name);

public sealed record AlbumDto(Guid Id, string Title, string ArtistName, int ReleaseYear);

public sealed record MusicaDto(Guid Id, string Title, string ArtistName, string AlbumTitle, int DurationSeconds);

public sealed record ResultadoBuscaCatalogoDto(IReadOnlyList<BandaDto> Artists, IReadOnlyList<MusicaDto> Tracks);


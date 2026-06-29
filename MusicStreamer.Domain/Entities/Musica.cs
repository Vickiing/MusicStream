namespace MusicStreamer.Domain.Entities;

public sealed class Musica
{
    public Guid Id { get; private set; }
    public Guid ArtistId { get; private set; }
    public Guid AlbumId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string NormalizedTitle { get; private set; } = string.Empty;
    public int DurationSeconds { get; private set; }

    public Banda Banda { get; private set; } = null!;
    public Album Album { get; private set; } = null!;

    private Musica()
    {
    }

    public Musica(Guid artistId, Guid albumId, string title, int durationSeconds)
    {
        Id = Guid.NewGuid();
        ArtistId = artistId;
        AlbumId = albumId;
        Title = title.Trim();
        NormalizedTitle = title.Trim().ToUpperInvariant();
        DurationSeconds = durationSeconds;
    }
}


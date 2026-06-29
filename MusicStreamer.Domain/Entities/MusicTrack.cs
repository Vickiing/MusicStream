namespace MusicStreamer.Domain.Entities;

public sealed class MusicTrack
{
    public Guid Id { get; private set; }
    public Guid ArtistId { get; private set; }
    public Guid AlbumId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string NormalizedTitle { get; private set; } = string.Empty;
    public int DurationSeconds { get; private set; }

    public Artist Artist { get; private set; } = null!;
    public Album Album { get; private set; } = null!;

    private MusicTrack()
    {
    }

    public MusicTrack(Guid artistId, Guid albumId, string title, int durationSeconds)
    {
        Id = Guid.NewGuid();
        ArtistId = artistId;
        AlbumId = albumId;
        Title = title.Trim();
        NormalizedTitle = title.Trim().ToUpperInvariant();
        DurationSeconds = durationSeconds;
    }
}

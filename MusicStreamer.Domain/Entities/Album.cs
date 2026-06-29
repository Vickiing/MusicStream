namespace MusicStreamer.Domain.Entities;

public sealed class Album
{
    public Guid Id { get; private set; }
    public Guid ArtistId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string NormalizedTitle { get; private set; } = string.Empty;
    public int ReleaseYear { get; private set; }

    public Artist Artist { get; private set; } = null!;
    public ICollection<MusicTrack> Tracks { get; private set; } = new List<MusicTrack>();

    private Album()
    {
    }

    public Album(Guid artistId, string title, int releaseYear)
    {
        Id = Guid.NewGuid();
        ArtistId = artistId;
        Title = title.Trim();
        NormalizedTitle = title.Trim().ToUpperInvariant();
        ReleaseYear = releaseYear;
    }
}

namespace MusicStreamer.Domain.Entities;

public sealed class Album
{
    public Guid Id { get; private set; }
    public Guid ArtistId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string NormalizedTitle { get; private set; } = string.Empty;
    public int ReleaseYear { get; private set; }

    public Banda Banda { get; private set; } = null!;
    public ICollection<Musica> Tracks { get; private set; } = new List<Musica>();

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


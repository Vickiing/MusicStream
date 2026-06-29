namespace MusicStreamer.Domain.Entities;

public sealed class Banda
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;

    public ICollection<Album> Albums { get; private set; } = new List<Album>();
    public ICollection<Musica> Tracks { get; private set; } = new List<Musica>();

    private Banda()
    {
    }

    public Banda(string name)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        NormalizedName = name.Trim().ToUpperInvariant();
    }
}


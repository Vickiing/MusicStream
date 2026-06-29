namespace MusicStreamer.Domain.Entities;

public sealed class FaixaPlaylist
{
    public Guid Id { get; private set; }
    public Guid PlaylistId { get; private set; }
    public Guid MusicId { get; private set; }
    public DateTimeOffset AddedAtUtc { get; private set; }

    public Playlist? Playlist { get; private set; }
    public Musica? Music { get; private set; }

    private FaixaPlaylist()
    {
    }

    public static FaixaPlaylist Create(Guid playlistId, Guid musicId)
    {
        return new FaixaPlaylist
        {
            Id = Guid.NewGuid(),
            PlaylistId = playlistId,
            MusicId = musicId,
            AddedAtUtc = DateTimeOffset.UtcNow
        };
    }
}


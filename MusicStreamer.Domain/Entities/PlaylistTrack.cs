namespace MusicStreamer.Domain.Entities;

public sealed class PlaylistTrack
{
    public Guid Id { get; private set; }
    public Guid PlaylistId { get; private set; }
    public Guid MusicId { get; private set; }
    public DateTimeOffset AddedAtUtc { get; private set; }

    public MusicTrack? Music { get; private set; }

    private PlaylistTrack()
    {
    }

    public static PlaylistTrack Create(Guid playlistId, Guid musicId)
    {
        return new PlaylistTrack
        {
            Id = Guid.NewGuid(),
            PlaylistId = playlistId,
            MusicId = musicId,
            AddedAtUtc = DateTimeOffset.UtcNow
        };
    }
}

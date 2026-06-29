namespace MusicStreamer.Domain.Entities;

public sealed class Playlist
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public ICollection<PlaylistTrack> Tracks { get; private set; } = new List<PlaylistTrack>();

    private Playlist()
    {
    }

    public static Playlist Create(Guid userAccountId, string name)
    {
        return new Playlist
        {
            Id = Guid.NewGuid(),
            UserAccountId = userAccountId,
            Name = name.Trim(),
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }

    public void AddTrack(Guid trackId)
    {
        if (Tracks.Any(item => item.MusicId == trackId))
        {
            return;
        }

        Tracks.Add(PlaylistTrack.Create(Id, trackId));
    }
}

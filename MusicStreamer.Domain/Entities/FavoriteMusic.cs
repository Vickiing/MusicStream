namespace MusicStreamer.Domain.Entities;

public sealed class FavoriteMusic
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }
    public Guid TrackId { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }

    private FavoriteMusic()
    {
    }

    public static FavoriteMusic Create(Guid userAccountId, Guid trackId)
    {
        return new FavoriteMusic
        {
            Id = Guid.NewGuid(),
            UserAccountId = userAccountId,
            TrackId = trackId,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}

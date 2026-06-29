namespace MusicStreamer.Domain.Entities;

public sealed class MusicaFavorita
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }
    public Guid TrackId { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }

    private MusicaFavorita()
    {
    }

    public static MusicaFavorita Create(Guid userAccountId, Guid trackId)
    {
        return new MusicaFavorita
        {
            Id = Guid.NewGuid(),
            UserAccountId = userAccountId,
            TrackId = trackId,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}


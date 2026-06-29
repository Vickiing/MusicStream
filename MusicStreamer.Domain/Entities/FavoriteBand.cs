namespace MusicStreamer.Domain.Entities;

public sealed class FavoriteBand
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }
    public Guid ArtistId { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }

    private FavoriteBand()
    {
    }

    public static FavoriteBand Create(Guid userAccountId, Guid artistId)
    {
        return new FavoriteBand
        {
            Id = Guid.NewGuid(),
            UserAccountId = userAccountId,
            ArtistId = artistId,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}

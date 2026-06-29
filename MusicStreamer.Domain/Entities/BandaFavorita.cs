namespace MusicStreamer.Domain.Entities;

public sealed class BandaFavorita
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }
    public Guid ArtistId { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }

    private BandaFavorita()
    {
    }

    public static BandaFavorita Create(Guid userAccountId, Guid artistId)
    {
        return new BandaFavorita
        {
            Id = Guid.NewGuid(),
            UserAccountId = userAccountId,
            ArtistId = artistId,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}


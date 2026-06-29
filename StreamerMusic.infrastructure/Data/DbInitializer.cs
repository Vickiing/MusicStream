using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.infrastructure.Data;

public sealed class DbInitializer(MusicStreamerDbContext dbContext)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await dbContext.SubscriptionPlans.AnyAsync(cancellationToken))
        {
            return;
        }

        var free = new SubscriptionPlan("Free", 0m, 30m, false);
        var premium = new SubscriptionPlan("Premium", 19.90m, 200m, true);
        var family = new SubscriptionPlan("Family", 29.90m, 350m, true);

        var artist1 = new Artist("Arctic Pulse");
        var artist2 = new Artist("Solar Echoes");

        var album1 = new Album(artist1.Id, "Northern Lights", 2024);
        var album2 = new Album(artist2.Id, "Afterglow", 2025);

        var track1 = new MusicTrack(artist1.Id, album1.Id, "City in Stereo", 215);
        var track2 = new MusicTrack(artist1.Id, album1.Id, "Midnight Transit", 198);
        var track3 = new MusicTrack(artist2.Id, album2.Id, "Gravity Bloom", 221);
        var track4 = new MusicTrack(artist2.Id, album2.Id, "Static Hearts", 204);

        var merchant1 = new Merchant("Loja Oficial MusicStreamer", "subscription");
        var merchant2 = new Merchant("Ingresso de Show", "ticketing");

        await dbContext.SubscriptionPlans.AddRangeAsync([free, premium, family], cancellationToken);
        await dbContext.Artists.AddRangeAsync([artist1, artist2], cancellationToken);
        await dbContext.Albums.AddRangeAsync([album1, album2], cancellationToken);
        await dbContext.MusicTracks.AddRangeAsync([track1, track2, track3, track4], cancellationToken);
        await dbContext.Merchants.AddRangeAsync([merchant1, merchant2], cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

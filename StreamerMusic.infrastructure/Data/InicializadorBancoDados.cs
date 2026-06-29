using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.infrastructure.Data;

public static class InicializadorBancoDados
{
    public static async Task MigrateAndSeedAsync(MusicStreamerDbContext context, CancellationToken cancellationToken = default)
    {
        await context.Database.MigrateAsync(cancellationToken);

        await SemearPlanosAsync(context, cancellationToken);
        await SemearComerciantesAsync(context, cancellationToken);
        await SemearCatalogoAsync(context, cancellationToken);
    }

    private static async Task SemearPlanosAsync(MusicStreamerDbContext context, CancellationToken cancellationToken)
    {
        var planos = new[]
        {
            new PlanoAssinatura("Gratuito", 0.00m, 10.00m, false),
            new PlanoAssinatura("Basico", 19.90m, 50.00m, false),
            new PlanoAssinatura("Premium", 39.90m, 100.00m, true)
        };

        foreach (var plano in planos)
        {
            await UpsertAsync(
                context.SubscriptionPlans,
                context,
                plano,
                plano.Id,
                cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task SemearComerciantesAsync(MusicStreamerDbContext context, CancellationToken cancellationToken)
    {
        var comerciantes = new[]
        {
            new Comerciante("Digital Store", "Music"),
            new Comerciante("Live Venue", "Entertainment"),
            new Comerciante("Music Market", "Retail")
        };

        foreach (var comerciante in comerciantes)
        {
            await UpsertAsync(
                context.Merchants,
                context,
                comerciante,
                comerciante.Id,
                cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task SemearCatalogoAsync(MusicStreamerDbContext context, CancellationToken cancellationToken)
    {
        var artistas = new[]
        {
            new Banda("Arctic Monkeys"),
            new Banda("Muse"),
            new Banda("Coldplay"),
            new Banda("Imagine Dragons"),
            new Banda("The Killers"),
            new Banda("Tame Impala"),
            new Banda("Paramore"),
            new Banda("Depeche Mode"),
            new Banda("Pearl Jam"),
            new Banda("The Black Keys")
        };

        foreach (var artista in artistas)
        {
            await UpsertAsync(context.Artists, context, artista, artista.Id, cancellationToken);
        }

        var albums = new[]
        {
            new Album(artistas[0].Id, "AM", 2013),
            new Album(artistas[1].Id, "Absolution", 2003),
            new Album(artistas[2].Id, "A Rush of Blood to the Head", 2002),
            new Album(artistas[3].Id, "Night Visions", 2012),
            new Album(artistas[4].Id, "Hot Fuss", 2004),
            new Album(artistas[5].Id, "Currents", 2015),
            new Album(artistas[6].Id, "Riot!", 2007),
            new Album(artistas[7].Id, "Violator", 1990),
            new Album(artistas[8].Id, "Ten", 1991),
            new Album(artistas[9].Id, "El Camino", 2011)
        };

        foreach (var album in albums)
        {
            await UpsertAsync(context.Albums, context, album, album.Id, cancellationToken);
        }

        var musicas = new[]
        {
            new Musica(artistas[0].Id, albums[0].Id, "Do I Wanna Know?", 272),
            new Musica(artistas[0].Id, albums[0].Id, "R U Mine?", 211),
            new Musica(artistas[0].Id, albums[0].Id, "Arabella", 208),
            new Musica(artistas[0].Id, albums[0].Id, "Why'd You Only Call Me When You're High?", 161),
            new Musica(artistas[0].Id, albums[0].Id, "One for the Road", 210),
            new Musica(artistas[1].Id, albums[1].Id, "Time Is Running Out", 237),
            new Musica(artistas[1].Id, albums[1].Id, "Hysteria", 227),
            new Musica(artistas[1].Id, albums[1].Id, "Stockholm Syndrome", 296),
            new Musica(artistas[1].Id, albums[1].Id, "Butterflies and Hurricanes", 285),
            new Musica(artistas[1].Id, albums[1].Id, "Sing for Absolution", 292),
            new Musica(artistas[2].Id, albums[2].Id, "Clocks", 307),
            new Musica(artistas[2].Id, albums[2].Id, "The Scientist", 309),
            new Musica(artistas[2].Id, albums[2].Id, "In My Place", 228),
            new Musica(artistas[2].Id, albums[2].Id, "Politik", 348),
            new Musica(artistas[2].Id, albums[2].Id, "Green Eyes", 223),
            new Musica(artistas[3].Id, albums[3].Id, "Radioactive", 186),
            new Musica(artistas[3].Id, albums[3].Id, "Demons", 177),
            new Musica(artistas[3].Id, albums[3].Id, "It's Time", 235),
            new Musica(artistas[3].Id, albums[3].Id, "On Top of the World", 196),
            new Musica(artistas[3].Id, albums[3].Id, "Amsterdam", 241),
            new Musica(artistas[4].Id, albums[4].Id, "Mr. Brightside", 223),
            new Musica(artistas[4].Id, albums[4].Id, "Somebody Told Me", 197),
            new Musica(artistas[4].Id, albums[4].Id, "Smile Like You Mean It", 235),
            new Musica(artistas[4].Id, albums[4].Id, "All These Things That I've Done", 300),
            new Musica(artistas[4].Id, albums[4].Id, "Change Your Mind", 220),
            new Musica(artistas[5].Id, albums[5].Id, "Let It Happen", 467),
            new Musica(artistas[5].Id, albums[5].Id, "The Less I Know the Better", 216),
            new Musica(artistas[5].Id, albums[5].Id, "Borderline", 238),
            new Musica(artistas[5].Id, albums[5].Id, "Eventually", 321),
            new Musica(artistas[5].Id, albums[5].Id, "New Person, Same Old Mistakes", 349),
            new Musica(artistas[6].Id, albums[6].Id, "Misery Business", 211),
            new Musica(artistas[6].Id, albums[6].Id, "That's What You Get", 216),
            new Musica(artistas[6].Id, albums[6].Id, "Crushcrushcrush", 216),
            new Musica(artistas[6].Id, albums[6].Id, "Decode", 263),
            new Musica(artistas[6].Id, albums[6].Id, "Ignorance", 218),
            new Musica(artistas[7].Id, albums[7].Id, "Personal Jesus", 225),
            new Musica(artistas[7].Id, albums[7].Id, "Enjoy the Silence", 257),
            new Musica(artistas[7].Id, albums[7].Id, "Policy of Truth", 236),
            new Musica(artistas[7].Id, albums[7].Id, "World in My Eyes", 296),
            new Musica(artistas[7].Id, albums[7].Id, "Halo", 266),
            new Musica(artistas[8].Id, albums[8].Id, "Alive", 341),
            new Musica(artistas[8].Id, albums[8].Id, "Even Flow", 293),
            new Musica(artistas[8].Id, albums[8].Id, "Black", 344),
            new Musica(artistas[8].Id, albums[8].Id, "Jeremy", 339),
            new Musica(artistas[8].Id, albums[8].Id, "Daughter", 213),
            new Musica(artistas[9].Id, albums[9].Id, "Tighten Up", 221),
            new Musica(artistas[9].Id, albums[9].Id, "Lonely Boy", 193),
            new Musica(artistas[9].Id, albums[9].Id, "Gold on the Ceiling", 194),
            new Musica(artistas[9].Id, albums[9].Id, "Little Black Submarines", 234),
            new Musica(artistas[9].Id, albums[9].Id, "Fever", 218)
        };

        foreach (var musica in musicas)
        {
            await UpsertAsync(context.MusicTracks, context, musica, musica.Id, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task UpsertAsync<TEntity>(DbSet<TEntity> set, MusicStreamerDbContext context, TEntity entity, Guid id, CancellationToken cancellationToken)
        where TEntity : class
    {
        var existing = await set.FindAsync(new object[] { id }, cancellationToken);
        if (existing is null)
        {
            set.Add(entity);
            return;
        }

        context.Entry(existing).CurrentValues.SetValues(entity);
    }
}

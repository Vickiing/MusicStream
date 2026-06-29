using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using System.Linq.Expressions;

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
                item => item.Name == plano.Name,
                plano,
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
                item => item.Name == comerciante.Name && item.Category == comerciante.Category,
                comerciante,
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

        var artistasPersistidos = new Dictionary<string, Banda>(StringComparer.OrdinalIgnoreCase);
        foreach (var artista in artistas)
        {
            var persistido = await UpsertAsync(
                context.Artists,
                context,
                item => item.NormalizedName == artista.NormalizedName,
                artista,
                cancellationToken);
            artistasPersistidos[artista.NormalizedName] = persistido;
        }

        var albums = new[]
        {
            new Album(artistasPersistidos["ARCTIC MONKEYS"].Id, "AM", 2013),
            new Album(artistasPersistidos["MUSE"].Id, "Absolution", 2003),
            new Album(artistasPersistidos["COLDPLAY"].Id, "A Rush of Blood to the Head", 2002),
            new Album(artistasPersistidos["IMAGINE DRAGONS"].Id, "Night Visions", 2012),
            new Album(artistasPersistidos["THE KILLERS"].Id, "Hot Fuss", 2004),
            new Album(artistasPersistidos["TAME IMPALA"].Id, "Currents", 2015),
            new Album(artistasPersistidos["PARAMORE"].Id, "Riot!", 2007),
            new Album(artistasPersistidos["DEPECHE MODE"].Id, "Violator", 1990),
            new Album(artistasPersistidos["PEARL JAM"].Id, "Ten", 1991),
            new Album(artistasPersistidos["THE BLACK KEYS"].Id, "El Camino", 2011)
        };

        var albumsPersistidos = new Dictionary<string, Album>(StringComparer.OrdinalIgnoreCase);
        foreach (var album in albums)
        {
            var persistido = await UpsertAsync(
                context.Albums,
                context,
                item => item.ArtistId == album.ArtistId && item.NormalizedTitle == album.NormalizedTitle,
                album,
                cancellationToken);
            albumsPersistidos[$"{album.ArtistId}:{album.NormalizedTitle}"] = persistido;
        }

        var musicas = new[]
        {
            new Musica(artistasPersistidos["ARCTIC MONKEYS"].Id, albumsPersistidos[$"{artistasPersistidos["ARCTIC MONKEYS"].Id}:AM"].Id, "Do I Wanna Know?", 272),
            new Musica(artistasPersistidos["ARCTIC MONKEYS"].Id, albumsPersistidos[$"{artistasPersistidos["ARCTIC MONKEYS"].Id}:AM"].Id, "R U Mine?", 211),
            new Musica(artistasPersistidos["ARCTIC MONKEYS"].Id, albumsPersistidos[$"{artistasPersistidos["ARCTIC MONKEYS"].Id}:AM"].Id, "Arabella", 208),
            new Musica(artistasPersistidos["ARCTIC MONKEYS"].Id, albumsPersistidos[$"{artistasPersistidos["ARCTIC MONKEYS"].Id}:AM"].Id, "Why'd You Only Call Me When You're High?", 161),
            new Musica(artistasPersistidos["ARCTIC MONKEYS"].Id, albumsPersistidos[$"{artistasPersistidos["ARCTIC MONKEYS"].Id}:AM"].Id, "One for the Road", 210),
            new Musica(artistasPersistidos["MUSE"].Id, albumsPersistidos[$"{artistasPersistidos["MUSE"].Id}:ABSOLUTION"].Id, "Time Is Running Out", 237),
            new Musica(artistasPersistidos["MUSE"].Id, albumsPersistidos[$"{artistasPersistidos["MUSE"].Id}:ABSOLUTION"].Id, "Hysteria", 227),
            new Musica(artistasPersistidos["MUSE"].Id, albumsPersistidos[$"{artistasPersistidos["MUSE"].Id}:ABSOLUTION"].Id, "Stockholm Syndrome", 296),
            new Musica(artistasPersistidos["MUSE"].Id, albumsPersistidos[$"{artistasPersistidos["MUSE"].Id}:ABSOLUTION"].Id, "Butterflies and Hurricanes", 285),
            new Musica(artistasPersistidos["MUSE"].Id, albumsPersistidos[$"{artistasPersistidos["MUSE"].Id}:ABSOLUTION"].Id, "Sing for Absolution", 292),
            new Musica(artistasPersistidos["COLDPLAY"].Id, albumsPersistidos[$"{artistasPersistidos["COLDPLAY"].Id}:A RUSH OF BLOOD TO THE HEAD"].Id, "Clocks", 307),
            new Musica(artistasPersistidos["COLDPLAY"].Id, albumsPersistidos[$"{artistasPersistidos["COLDPLAY"].Id}:A RUSH OF BLOOD TO THE HEAD"].Id, "The Scientist", 309),
            new Musica(artistasPersistidos["COLDPLAY"].Id, albumsPersistidos[$"{artistasPersistidos["COLDPLAY"].Id}:A RUSH OF BLOOD TO THE HEAD"].Id, "In My Place", 228),
            new Musica(artistasPersistidos["COLDPLAY"].Id, albumsPersistidos[$"{artistasPersistidos["COLDPLAY"].Id}:A RUSH OF BLOOD TO THE HEAD"].Id, "Politik", 348),
            new Musica(artistasPersistidos["COLDPLAY"].Id, albumsPersistidos[$"{artistasPersistidos["COLDPLAY"].Id}:A RUSH OF BLOOD TO THE HEAD"].Id, "Green Eyes", 223),
            new Musica(artistasPersistidos["IMAGINE DRAGONS"].Id, albumsPersistidos[$"{artistasPersistidos["IMAGINE DRAGONS"].Id}:NIGHT VISIONS"].Id, "Radioactive", 186),
            new Musica(artistasPersistidos["IMAGINE DRAGONS"].Id, albumsPersistidos[$"{artistasPersistidos["IMAGINE DRAGONS"].Id}:NIGHT VISIONS"].Id, "Demons", 177),
            new Musica(artistasPersistidos["IMAGINE DRAGONS"].Id, albumsPersistidos[$"{artistasPersistidos["IMAGINE DRAGONS"].Id}:NIGHT VISIONS"].Id, "It's Time", 235),
            new Musica(artistasPersistidos["IMAGINE DRAGONS"].Id, albumsPersistidos[$"{artistasPersistidos["IMAGINE DRAGONS"].Id}:NIGHT VISIONS"].Id, "On Top of the World", 196),
            new Musica(artistasPersistidos["IMAGINE DRAGONS"].Id, albumsPersistidos[$"{artistasPersistidos["IMAGINE DRAGONS"].Id}:NIGHT VISIONS"].Id, "Amsterdam", 241),
            new Musica(artistasPersistidos["THE KILLERS"].Id, albumsPersistidos[$"{artistasPersistidos["THE KILLERS"].Id}:HOT FUSS"].Id, "Mr. Brightside", 223),
            new Musica(artistasPersistidos["THE KILLERS"].Id, albumsPersistidos[$"{artistasPersistidos["THE KILLERS"].Id}:HOT FUSS"].Id, "Somebody Told Me", 197),
            new Musica(artistasPersistidos["THE KILLERS"].Id, albumsPersistidos[$"{artistasPersistidos["THE KILLERS"].Id}:HOT FUSS"].Id, "Smile Like You Mean It", 235),
            new Musica(artistasPersistidos["THE KILLERS"].Id, albumsPersistidos[$"{artistasPersistidos["THE KILLERS"].Id}:HOT FUSS"].Id, "All These Things That I've Done", 300),
            new Musica(artistasPersistidos["THE KILLERS"].Id, albumsPersistidos[$"{artistasPersistidos["THE KILLERS"].Id}:HOT FUSS"].Id, "Change Your Mind", 220),
            new Musica(artistasPersistidos["TAME IMPALA"].Id, albumsPersistidos[$"{artistasPersistidos["TAME IMPALA"].Id}:CURRENTS"].Id, "Let It Happen", 467),
            new Musica(artistasPersistidos["TAME IMPALA"].Id, albumsPersistidos[$"{artistasPersistidos["TAME IMPALA"].Id}:CURRENTS"].Id, "The Less I Know the Better", 216),
            new Musica(artistasPersistidos["TAME IMPALA"].Id, albumsPersistidos[$"{artistasPersistidos["TAME IMPALA"].Id}:CURRENTS"].Id, "Borderline", 238),
            new Musica(artistasPersistidos["TAME IMPALA"].Id, albumsPersistidos[$"{artistasPersistidos["TAME IMPALA"].Id}:CURRENTS"].Id, "Eventually", 321),
            new Musica(artistasPersistidos["TAME IMPALA"].Id, albumsPersistidos[$"{artistasPersistidos["TAME IMPALA"].Id}:CURRENTS"].Id, "New Person, Same Old Mistakes", 349),
            new Musica(artistasPersistidos["PARAMORE"].Id, albumsPersistidos[$"{artistasPersistidos["PARAMORE"].Id}:RIOT!"].Id, "Misery Business", 211),
            new Musica(artistasPersistidos["PARAMORE"].Id, albumsPersistidos[$"{artistasPersistidos["PARAMORE"].Id}:RIOT!"].Id, "That's What You Get", 216),
            new Musica(artistasPersistidos["PARAMORE"].Id, albumsPersistidos[$"{artistasPersistidos["PARAMORE"].Id}:RIOT!"].Id, "Crushcrushcrush", 216),
            new Musica(artistasPersistidos["PARAMORE"].Id, albumsPersistidos[$"{artistasPersistidos["PARAMORE"].Id}:RIOT!"].Id, "Decode", 263),
            new Musica(artistasPersistidos["PARAMORE"].Id, albumsPersistidos[$"{artistasPersistidos["PARAMORE"].Id}:RIOT!"].Id, "Ignorance", 218),
            new Musica(artistasPersistidos["DEPECHE MODE"].Id, albumsPersistidos[$"{artistasPersistidos["DEPECHE MODE"].Id}:VIOLATOR"].Id, "Personal Jesus", 225),
            new Musica(artistasPersistidos["DEPECHE MODE"].Id, albumsPersistidos[$"{artistasPersistidos["DEPECHE MODE"].Id}:VIOLATOR"].Id, "Enjoy the Silence", 257),
            new Musica(artistasPersistidos["DEPECHE MODE"].Id, albumsPersistidos[$"{artistasPersistidos["DEPECHE MODE"].Id}:VIOLATOR"].Id, "Policy of Truth", 236),
            new Musica(artistasPersistidos["DEPECHE MODE"].Id, albumsPersistidos[$"{artistasPersistidos["DEPECHE MODE"].Id}:VIOLATOR"].Id, "World in My Eyes", 296),
            new Musica(artistasPersistidos["DEPECHE MODE"].Id, albumsPersistidos[$"{artistasPersistidos["DEPECHE MODE"].Id}:VIOLATOR"].Id, "Halo", 266),
            new Musica(artistasPersistidos["PEARL JAM"].Id, albumsPersistidos[$"{artistasPersistidos["PEARL JAM"].Id}:TEN"].Id, "Alive", 341),
            new Musica(artistasPersistidos["PEARL JAM"].Id, albumsPersistidos[$"{artistasPersistidos["PEARL JAM"].Id}:TEN"].Id, "Even Flow", 293),
            new Musica(artistasPersistidos["PEARL JAM"].Id, albumsPersistidos[$"{artistasPersistidos["PEARL JAM"].Id}:TEN"].Id, "Black", 344),
            new Musica(artistasPersistidos["PEARL JAM"].Id, albumsPersistidos[$"{artistasPersistidos["PEARL JAM"].Id}:TEN"].Id, "Jeremy", 339),
            new Musica(artistasPersistidos["PEARL JAM"].Id, albumsPersistidos[$"{artistasPersistidos["PEARL JAM"].Id}:TEN"].Id, "Daughter", 213),
            new Musica(artistasPersistidos["THE BLACK KEYS"].Id, albumsPersistidos[$"{artistasPersistidos["THE BLACK KEYS"].Id}:EL CAMINO"].Id, "Tighten Up", 221),
            new Musica(artistasPersistidos["THE BLACK KEYS"].Id, albumsPersistidos[$"{artistasPersistidos["THE BLACK KEYS"].Id}:EL CAMINO"].Id, "Lonely Boy", 193),
            new Musica(artistasPersistidos["THE BLACK KEYS"].Id, albumsPersistidos[$"{artistasPersistidos["THE BLACK KEYS"].Id}:EL CAMINO"].Id, "Gold on the Ceiling", 194),
            new Musica(artistasPersistidos["THE BLACK KEYS"].Id, albumsPersistidos[$"{artistasPersistidos["THE BLACK KEYS"].Id}:EL CAMINO"].Id, "Little Black Submarines", 234),
            new Musica(artistasPersistidos["THE BLACK KEYS"].Id, albumsPersistidos[$"{artistasPersistidos["THE BLACK KEYS"].Id}:EL CAMINO"].Id, "Fever", 218)
        };

        foreach (var musica in musicas)
        {
            await UpsertAsync(
                context.MusicTracks,
                context,
                item => item.ArtistId == musica.ArtistId && item.AlbumId == musica.AlbumId && item.NormalizedTitle == musica.NormalizedTitle,
                musica,
                cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task<TEntity> UpsertAsync<TEntity>(
        DbSet<TEntity> set,
        MusicStreamerDbContext context,
        Expression<Func<TEntity, bool>> predicate,
        TEntity entity,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var existing = await set.FirstOrDefaultAsync(predicate, cancellationToken);
        if (existing is null)
        {
            set.Add(entity);
            return entity;
        }

        return existing;
    }
}

using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;
using MusicStreamer.infrastructure.Data;

namespace MusicStreamer.infrastructure.Repositories;

public sealed class FavoritesRepository(MusicStreamerDbContext dbContext) : IFavoritesRepository
{
    public async Task AddFavoriteTrackAsync(FavoriteMusic favoriteMusic, CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.FavoriteMusics.AnyAsync(
            item => item.UserAccountId == favoriteMusic.UserAccountId && item.TrackId == favoriteMusic.TrackId,
            cancellationToken);

        if (!exists)
        {
            dbContext.FavoriteMusics.Add(favoriteMusic);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoveFavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default)
    {
        var existing = await dbContext.FavoriteMusics.FirstOrDefaultAsync(
            item => item.UserAccountId == userId && item.TrackId == trackId,
            cancellationToken);

        if (existing is not null)
        {
            dbContext.FavoriteMusics.Remove(existing);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task AddFavoriteArtistAsync(FavoriteBand favoriteBand, CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.FavoriteBands.AnyAsync(
            item => item.UserAccountId == favoriteBand.UserAccountId && item.ArtistId == favoriteBand.ArtistId,
            cancellationToken);

        if (!exists)
        {
            dbContext.FavoriteBands.Add(favoriteBand);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoveFavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default)
    {
        var existing = await dbContext.FavoriteBands.FirstOrDefaultAsync(
            item => item.UserAccountId == userId && item.ArtistId == artistId,
            cancellationToken);

        if (existing is not null)
        {
            dbContext.FavoriteBands.Remove(existing);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<(IReadOnlyList<FavoriteMusic> FavoriteTracks, IReadOnlyList<FavoriteBand> FavoriteBands)> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tracks = await dbContext.FavoriteMusics
            .AsNoTracking()
            .Where(item => item.UserAccountId == userId)
            .ToListAsync(cancellationToken);

        var artists = await dbContext.FavoriteBands
            .AsNoTracking()
            .Where(item => item.UserAccountId == userId)
            .ToListAsync(cancellationToken);

        return (tracks, artists);
    }
}

using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class FavoritesService(
    IContaUsuarioRepository userAccountRepository,
    ICatalogoRepository catalogRepository,
    IRepositorioFavoritos favoritesRepository) : IServicoFavoritos
{
    public async Task<ResumoFavoritosDto> FavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default)
    {
        await EnsureUserAndTrackAsync(userId, trackId, cancellationToken);
        await favoritesRepository.AddFavoriteTrackAsync(MusicaFavorita.Create(userId, trackId), cancellationToken);
        return await BuildSummaryAsync(userId, cancellationToken);
    }

    public async Task<ResumoFavoritosDto> UnfavoriteTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken = default)
    {
        await EnsureUserAndTrackAsync(userId, trackId, cancellationToken);
        await favoritesRepository.RemoveFavoriteTrackAsync(userId, trackId, cancellationToken);
        return await BuildSummaryAsync(userId, cancellationToken);
    }

    public async Task<ResumoFavoritosDto> FavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default)
    {
        await EnsureUserAndArtistAsync(userId, artistId, cancellationToken);
        await favoritesRepository.AddFavoriteArtistAsync(BandaFavorita.Create(userId, artistId), cancellationToken);
        return await BuildSummaryAsync(userId, cancellationToken);
    }

    public async Task<ResumoFavoritosDto> UnfavoriteArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken = default)
    {
        await EnsureUserAndArtistAsync(userId, artistId, cancellationToken);
        await favoritesRepository.RemoveFavoriteArtistAsync(userId, artistId, cancellationToken);
        return await BuildSummaryAsync(userId, cancellationToken);
    }

    private async Task EnsureUserAndTrackAsync(Guid userId, Guid trackId, CancellationToken cancellationToken)
    {
        var user = await userAccountRepository.GetByIdAsync(userId, cancellationToken);
        var track = await catalogRepository.GetTrackByIdAsync(trackId, cancellationToken);
        if (user is null || track is null)
        {
            throw new InvalidOperationException("Usuario ou musica nao encontrado.");
        }
    }

    private async Task EnsureUserAndArtistAsync(Guid userId, Guid artistId, CancellationToken cancellationToken)
    {
        var user = await userAccountRepository.GetByIdAsync(userId, cancellationToken);
        var artist = await catalogRepository.GetArtistByIdAsync(artistId, cancellationToken);
        if (user is null || artist is null)
        {
            throw new InvalidOperationException("Usuario ou banda nao encontrada.");
        }
    }

    private async Task<ResumoFavoritosDto> BuildSummaryAsync(Guid userId, CancellationToken cancellationToken)
    {
        var favorites = await favoritesRepository.GetByUserAsync(userId, cancellationToken);
        return new ResumoFavoritosDto(
            userId,
            favorites.FavoriteTracks.Select(item => item.TrackId).ToList(),
            favorites.FavoriteBands.Select(item => item.ArtistId).ToList());
    }
}


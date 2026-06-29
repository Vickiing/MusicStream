using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class ServicoPlaylist(
    IContaUsuarioRepository userAccountRepository,
    ICatalogoRepository catalogRepository,
    IPlaylistRepository playlistRepository) : IServicoPlaylist
{
    public async Task<PlaylistDto> CreateAsync(CriarPlaylistDto input, CancellationToken cancellationToken = default)
    {
        var user = await userAccountRepository.GetByIdAsync(input.UserId, cancellationToken);
        if (user is null)
        {
            throw new InvalidOperationException("Usuario nao encontrado.");
        }

        var playlist = Playlist.Create(input.UserId, input.Name);
        await playlistRepository.AddAsync(playlist, cancellationToken);
        return Map(playlist);
    }

    public async Task<PlaylistDto?> AddTrackAsync(AdicionarMusicaNaPlaylistDto input, CancellationToken cancellationToken = default)
    {
        var playlist = await playlistRepository.GetByIdAsync(input.PlaylistId, cancellationToken);
        var track = await catalogRepository.GetTrackByIdAsync(input.TrackId, cancellationToken);

        if (playlist is null || track is null)
        {
            return null;
        }

        playlist.AddTrack(track.Id);
        await playlistRepository.UpdateAsync(playlist, cancellationToken);

        var refreshed = await playlistRepository.GetByIdAsync(input.PlaylistId, cancellationToken);
        return refreshed is null ? null : Map(refreshed);
    }

    public async Task<IReadOnlyList<PlaylistDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var playlists = await playlistRepository.GetByUserAsync(userId, cancellationToken);
        return playlists.Select(Map).ToList();
    }

    private static PlaylistDto Map(Playlist playlist)
    {
        return new PlaylistDto(
            playlist.Id,
            playlist.UserAccountId,
            playlist.Name,
            playlist.Tracks
                .Select(track => new FaixaPlaylistDto(
                    track.MusicId,
                    track.Music?.Title ?? string.Empty,
                    track.Music?.Banda?.Name ?? string.Empty,
                    track.Music?.Album?.Title ?? string.Empty))
                .ToList());
    }
}



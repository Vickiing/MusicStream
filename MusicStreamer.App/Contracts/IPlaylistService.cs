using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IServicoPlaylist
{
    Task<PlaylistDto> CreateAsync(CriarPlaylistDto input, CancellationToken cancellationToken = default);
    Task<PlaylistDto?> AddTrackAsync(AdicionarMusicaNaPlaylistDto input, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlaylistDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}


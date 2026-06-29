using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IPlaylistService
{
    Task<PlaylistDto> CreateAsync(CreatePlaylistDto input, CancellationToken cancellationToken = default);
    Task<PlaylistDto?> AddTrackAsync(AddTrackToPlaylistDto input, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlaylistDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}

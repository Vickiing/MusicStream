using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface IPlaylistRepository
{
    Task AddAsync(Playlist playlist, CancellationToken cancellationToken = default);
    Task<Playlist?> GetByIdAsync(Guid playlistId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Playlist>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> AddTrackAsync(Guid playlistId, Guid trackId, CancellationToken cancellationToken = default);
}

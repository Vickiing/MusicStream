using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Repositories;

public interface ICatalogoRepository
{
    Task<IReadOnlyList<Banda>> GetArtistsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Album>> GetAlbumsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Musica>> GetTracksAsync(CancellationToken cancellationToken = default);
    Task<Banda?> GetArtistByIdAsync(Guid artistId, CancellationToken cancellationToken = default);
    Task<Musica?> GetTrackByIdAsync(Guid trackId, CancellationToken cancellationToken = default);
    Task<ResultadoBuscaCatalogo> SearchAsync(string term, CancellationToken cancellationToken = default);
}


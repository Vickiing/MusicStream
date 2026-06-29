using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IServicoCatalogo
{
    Task<IReadOnlyList<BandaDto>> GetArtistsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AlbumDto>> GetAlbumsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MusicaDto>> GetTracksAsync(CancellationToken cancellationToken = default);
    Task<ResultadoBuscaCatalogoDto> SearchAsync(string term, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
}


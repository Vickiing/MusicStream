using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IServicoComerciantes
{
    Task<IReadOnlyList<ComercianteDto>> GetAllAsync(CancellationToken cancellationToken = default);
}


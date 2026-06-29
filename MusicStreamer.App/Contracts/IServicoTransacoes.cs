using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IServicoTransacoes
{
    Task<ResultadoTransacaoDto?> AuthorizeAsync(AutorizarTransacaoDto input, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResultadoTransacaoDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}


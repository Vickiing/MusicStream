using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IServicoAutenticacao
{
    Task<RespostaAutenticacaoDto> RegisterAsync(CadastrarUsuarioDto input, CancellationToken cancellationToken = default);
    Task<RespostaAutenticacaoDto?> LoginAsync(EntrarDto input, CancellationToken cancellationToken = default);
}


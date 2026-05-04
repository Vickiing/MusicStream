using MusicStreamer.Domain.Entity;

namespace MusicStreamer.Domain.Contracts
{
    public interface IAssinaturaRepository
    {
        Task<AssinaturaEntity?> CadastrarAssinaturaAsync(AssinaturaEntity entity);
        Task<bool> CancelarAssinaturaUsuarioAsync(int usuarioId);
    }
}

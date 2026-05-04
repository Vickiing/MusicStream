using MusicStreamer.Domain.Entity;

namespace MusicStreamer.Domain.Contracts
{
    public interface IPlanoAssinaturaDomainService
    {
        Task<bool> PodeAtivarAssinaturaAsync(UsuarioEntity usuario);
    }
}

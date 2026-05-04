using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.Domain.Services
{
    public class PlanoAssinaturaDomainService : IPlanoAssinaturaDomainService
    {
        public Task<bool> PodeAtivarAssinaturaAsync(UsuarioEntity usuario)
        {
            var usuarioAtivo = usuario.AssinaturaAtiva;
            return Task.FromResult(!usuarioAtivo);
        }
    }
}

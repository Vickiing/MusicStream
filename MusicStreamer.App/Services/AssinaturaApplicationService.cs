using MusicStreamer.App.Contracts;
using MusicStreamer.App.Inputs;
using MusicStreamer.Domain.Contracts;
using MusicStreamer.Domain.Entity;

namespace MusicStreamer.App.Services
{
    public class AssinaturaApplicationService(
        IAssinaturaRepository assinaturaRepository,
        IUsuarioRepository usuarioRepository,
        IPlanoAssinaturaDomainService planoAssinatura,
        IPagamentoApplicationService pagamentoService) : IAssinaturaApplicationService
    {
        private readonly IAssinaturaRepository _assinaturaRepository = assinaturaRepository;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IPlanoAssinaturaDomainService _planoAssinatura = planoAssinatura;
        private readonly IPagamentoApplicationService _pagamentoService = pagamentoService;

        public async Task<bool> AtivarAssinaturaAsync(AtivarAssinaturaInput ativarAssinaturaInput)
        {
            if (ativarAssinaturaInput is null)
            {
                return false;
            }

            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(ativarAssinaturaInput.UsuarioId);
            if (usuario is null)
            {
                return false;
            }

            var podeAtivar = await _planoAssinatura.PodeAtivarAssinaturaAsync(usuario);
            if (!podeAtivar)
            {
                return false;
            }

            var pagamentoProcessado = await _pagamentoService.ProcessarPagamentoAsync(ativarAssinaturaInput);
            if (!pagamentoProcessado)
            {
                return false;
            }

            var assinatura = new AssinaturaEntity
            {
                UsuarioId = usuario.Id,
                TipoAssinatura = ativarAssinaturaInput.TipoAssinatura,
                Ativa = true,
                DataInicio = DateTime.UtcNow,
                DataFim = DateTime.UtcNow.AddMonths(1),
                RenovacaoAutomatica = ativarAssinaturaInput.RenovacaoAutomatica,
                DataFimAutomatica = ativarAssinaturaInput.RenovacaoAutomatica ? DateTime.UtcNow.AddMonths(2) : DateTime.UtcNow.AddMonths(1)
            };

            var assinaturaSalva = await _assinaturaRepository.CadastrarAssinaturaAsync(assinatura);
            if (assinaturaSalva is null)
            {
                return false;
            }

            usuario.AssinaturaAtiva = true;
            usuario.AssinaturaId = assinaturaSalva.Id;

            var usuarioAtualizado = await _usuarioRepository.AtualizarUsuarioAsync(usuario);
            return usuarioAtualizado;
        }

        public async Task<bool> CancelarAssinaturaAsync(int usuarioId)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(usuarioId);
            if (usuario is null)
            {
                return false;
            }

            var cancelado = await _assinaturaRepository.CancelarAssinaturaUsuarioAsync(usuarioId);
            if (!cancelado)
            {
                return false;
            }

            usuario.AssinaturaAtiva = false;
            usuario.AssinaturaId = null;

            return await _usuarioRepository.AtualizarUsuarioAsync(usuario);
        }
    }
}

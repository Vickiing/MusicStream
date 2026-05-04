using MusicStreamer.App.Contracts;
using MusicStreamer.App.Inputs;

namespace MusicStreamer.App.Services
{
    public class PagamentoApplicationService : IPagamentoApplicationService
    {
        public Task<bool> ProcessarPagamentoAsync(AtivarAssinaturaInput ativarAssinaturaInput)
        {
            if (ativarAssinaturaInput is null)
            {
                return Task.FromResult(false);
            }

            if (ativarAssinaturaInput.UsuarioId <= 0 || ativarAssinaturaInput.TipoPagamento <= 0 || ativarAssinaturaInput.TipoAssinatura <= 0)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}

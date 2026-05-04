using MusicStreamer.App.Inputs;

namespace MusicStreamer.App.Contracts
{
    public interface IPagamentoApplicationService
    {
        Task<bool> ProcessarPagamentoAsync(AtivarAssinaturaInput ativarAssinaturaInput);
    }
}

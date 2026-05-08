using MusicStreamer.App.Inputs;

namespace MusicStreamer.App.Contracts
{
    public interface IAssinaturaApplicationService
    {
        Task<bool> AtivarAssinaturaAsync(AtivarAssinaturaInput ativarAssinaturaInput);
        Task<bool> CancelarAssinaturaAsync(Guid usuarioId);
    }
}

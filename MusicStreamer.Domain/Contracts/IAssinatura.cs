using MusicStreamer.Domain.Entity;

namespace MusicStreamer.infrastructure.Contract
{
    public interface IAssinatura
    {
        Task<bool> AtivarAssinatura(AssinaturaEntity entity);
        Task<bool> DesativarAssinatura(int id);

    }
}

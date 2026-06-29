using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Services;

public interface IServicoAutorizacaoTransacao
{
    DecisaoAutorizacaoTransacao Authorize(
        ContaUsuario account,
        PlanoAssinatura? plan,
        Comerciante merchant,
        decimal amount,
        DateTimeOffset requestedAtUtc,
        Transaction? lastApprovedTransaction);
}


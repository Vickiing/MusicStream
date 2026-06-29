using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IServicoPlanosAssinatura
{
    Task<IReadOnlyList<PlanoAssinaturaDto>> GetPlansAsync(CancellationToken cancellationToken = default);
    Task<PlanoAssinaturaDto?> GetPlanByIdAsync(Guid planId, CancellationToken cancellationToken = default);
    Task<AssinaturaUsuarioDto?> ChoosePlanAsync(EscolherPlanoDto input, CancellationToken cancellationToken = default);
}


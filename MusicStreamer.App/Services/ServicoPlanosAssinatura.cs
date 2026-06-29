using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class ServicoPlanosAssinatura(
    IContaUsuarioRepository userAccountRepository,
    IPlanoAssinaturaRepository subscriptionPlanRepository) : IServicoPlanosAssinatura
{
    private static readonly IReadOnlyList<PlanoAssinaturaDto> DefaultPlans =
    [
        new PlanoAssinaturaDto(Guid.Parse("40000000-0000-0000-0000-000000000001"), "Gratuito", 0m, 10m, false),
        new PlanoAssinaturaDto(Guid.Parse("40000000-0000-0000-0000-000000000002"), "Basico", 19.90m, 50m, false),
        new PlanoAssinaturaDto(Guid.Parse("40000000-0000-0000-0000-000000000003"), "Premium", 39.90m, 150m, true)
    ];

    public async Task<IReadOnlyList<PlanoAssinaturaDto>> GetPlansAsync(CancellationToken cancellationToken = default)
    {
        var plans = await subscriptionPlanRepository.GetAllAsync(cancellationToken);
        if (plans.Count == 0)
        {
            return DefaultPlans;
        }

        return plans
            .Select(plan => new PlanoAssinaturaDto(
                plan.Id,
                plan.Name,
                plan.MonthlyPrice,
                plan.MaxTransactionAmount,
                plan.AllowsNightTransactions))
            .ToList();
    }

    public async Task<PlanoAssinaturaDto?> GetPlanByIdAsync(Guid planId, CancellationToken cancellationToken = default)
    {
        var plans = await GetPlansAsync(cancellationToken);
        return plans.FirstOrDefault(plan => plan.Id == planId);
    }

    public async Task<AssinaturaUsuarioDto?> ChoosePlanAsync(EscolherPlanoDto input, CancellationToken cancellationToken = default)
    {
        var user = await userAccountRepository.GetByIdAsync(input.UserId, cancellationToken);
        var plan = await GetPlanByIdAsync(input.PlanId, cancellationToken);

        if (user is null || plan is null)
        {
            return null;
        }

        user.AssignSubscription(plan.Id);
        await userAccountRepository.UpdateAsync(user, cancellationToken);

        return new AssinaturaUsuarioDto(user.Id, plan.Id, plan.Name, user.SubscriptionPlanId.HasValue);
    }
}



using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;
using MusicStreamer.Domain.Repositories;

namespace MusicStreamer.App.Services;

public sealed class SubscriptionService(
    IUserAccountRepository userAccountRepository,
    ISubscriptionPlanRepository subscriptionPlanRepository) : ISubscriptionService
{
    public async Task<IReadOnlyList<SubscriptionPlanDto>> GetPlansAsync(CancellationToken cancellationToken = default)
    {
        var plans = await subscriptionPlanRepository.GetAllAsync(cancellationToken);
        return plans
            .Select(plan => new SubscriptionPlanDto(
                plan.Id,
                plan.Name,
                plan.MonthlyPrice,
                plan.MaxTransactionAmount,
                plan.AllowsNightTransactions))
            .ToList();
    }

    public async Task<UserSubscriptionDto?> ChoosePlanAsync(ChoosePlanDto input, CancellationToken cancellationToken = default)
    {
        var user = await userAccountRepository.GetByIdAsync(input.UserId, cancellationToken);
        var plan = await subscriptionPlanRepository.GetByIdAsync(input.PlanId, cancellationToken);

        if (user is null || plan is null)
        {
            return null;
        }

        user.AssignSubscription(plan.Id);
        await userAccountRepository.UpdateAsync(user, cancellationToken);

        return new UserSubscriptionDto(user.Id, plan.Id, plan.Name, user.SubscriptionPlanId.HasValue);
    }
}

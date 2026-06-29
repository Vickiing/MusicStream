namespace MusicStreamer.App.DTOs;

public sealed record SubscriptionPlanDto(Guid Id, string Name, decimal MonthlyPrice, decimal MaxTransactionAmount, bool AllowsNightTransactions);

public sealed record ChoosePlanDto(Guid UserId, Guid PlanId);

public sealed record UserSubscriptionDto(Guid UserId, Guid PlanId, string PlanName, bool HasActiveSubscription);

namespace MusicStreamer.App.DTOs;

public sealed record PlanoAssinaturaDto(Guid Id, string Name, decimal MonthlyPrice, decimal MaxTransactionAmount, bool AllowsNightTransactions);

public sealed record EscolherPlanoDto(Guid UserId, Guid PlanId);

public sealed record AssinaturaUsuarioDto(Guid UserId, Guid PlanId, string PlanName, bool HasActiveSubscription);


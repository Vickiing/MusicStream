using MusicStreamer.Domain.Entities;
using MusicStreamer.Domain.Enums;

namespace MusicStreamer.Domain.Services;

public sealed class TransactionAuthorizationDomainService : ITransactionAuthorizationDomainService
{
    public TransactionAuthorizationDecision Authorize(
        UserAccount account,
        SubscriptionPlan? plan,
        Merchant merchant,
        decimal amount,
        DateTimeOffset requestedAtUtc,
        Transaction? lastApprovedTransaction)
    {
        if (account.Status != AccountStatus.Active)
        {
            return new TransactionAuthorizationDecision(false, "negada: conta inativa");
        }

        if (plan is null || !plan.IsActive)
        {
            return new TransactionAuthorizationDecision(false, "negada: assinatura inexistente");
        }

        if (!merchant.IsActive)
        {
            return new TransactionAuthorizationDecision(false, "negada: comerciante inativo");
        }

        if (amount <= 0)
        {
            return new TransactionAuthorizationDecision(false, "negada: valor invalido");
        }

        if (amount > plan.MaxTransactionAmount)
        {
            return new TransactionAuthorizationDecision(false, "negada: valor acima do limite do plano");
        }

        if (requestedAtUtc.Hour < 6 && !plan.AllowsNightTransactions)
        {
            return new TransactionAuthorizationDecision(false, "negada: horario fora da politica do plano");
        }

        if (lastApprovedTransaction is not null
            && lastApprovedTransaction.MerchantId == merchant.Id
            && lastApprovedTransaction.Amount == amount
            && requestedAtUtc - lastApprovedTransaction.RequestedAtUtc <= TimeSpan.FromMinutes(2))
        {
            return new TransactionAuthorizationDecision(false, "negada: tentativa duplicada apos ultima autorizacao");
        }

        return new TransactionAuthorizationDecision(true, "aprovada");
    }
}

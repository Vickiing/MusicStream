using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Domain.Services;

public interface ITransactionAuthorizationDomainService
{
    TransactionAuthorizationDecision Authorize(
        UserAccount account,
        SubscriptionPlan? plan,
        Merchant merchant,
        decimal amount,
        DateTimeOffset requestedAtUtc,
        Transaction? lastApprovedTransaction);
}

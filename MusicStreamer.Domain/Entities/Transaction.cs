using MusicStreamer.Domain.Enums;

namespace MusicStreamer.Domain.Entities;

public sealed class Transaction
{
    public Guid Id { get; private set; }
    public Guid UserAccountId { get; private set; }
    public Guid MerchantId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionStatus Status { get; private set; }
    public DateTimeOffset RequestedAtUtc { get; private set; }
    public DateTimeOffset? AuthorizedAtUtc { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public ICollection<TransactionNotification> Notifications { get; private set; } = new List<TransactionNotification>();

    private Transaction()
    {
    }

    public static Transaction Create(Guid userAccountId, Guid merchantId, decimal amount, DateTimeOffset requestedAtUtc, bool approved, string reason)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            UserAccountId = userAccountId,
            MerchantId = merchantId,
            Amount = amount,
            RequestedAtUtc = requestedAtUtc,
            AuthorizedAtUtc = approved ? DateTimeOffset.UtcNow : null,
            Status = approved ? TransactionStatus.Approved : TransactionStatus.Denied,
            Reason = reason
        };
    }

    public void AddNotification(TransactionNotification notification)
    {
        Notifications.Add(notification);
    }
}

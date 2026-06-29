using MusicStreamer.Domain.Enums;
using MusicStreamer.Domain.Services;

namespace MusicStreamer.Domain.Entities;

public sealed class TransactionNotification
{
    public Guid Id { get; private set; }
    public Guid TransactionId { get; private set; }
    public string Recipient { get; private set; } = string.Empty;
    public string Channel { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public NotificationStatus Status { get; private set; }
    public DateTimeOffset SentAtUtc { get; private set; }

    private TransactionNotification()
    {
    }

    public static TransactionNotification CreateMerchantNotification(Guid transactionId, string merchantName, TransactionAuthorizationDecision decision)
    {
        return Create(transactionId, merchantName, "MerchantWebhook", $"Transacao {decision.Reason}", NotificationStatus.Delivered);
    }

    public static TransactionNotification CreateCardOwnerNotification(Guid transactionId, string email, TransactionAuthorizationDecision decision)
    {
        return Create(transactionId, email, "Email", $"Sua transacao foi {decision.Reason}", NotificationStatus.Delivered);
    }

    private static TransactionNotification Create(Guid transactionId, string recipient, string channel, string message, NotificationStatus status)
    {
        return new TransactionNotification
        {
            Id = Guid.NewGuid(),
            TransactionId = transactionId,
            Recipient = recipient,
            Channel = channel,
            Message = message,
            Status = status,
            SentAtUtc = DateTimeOffset.UtcNow
        };
    }
}

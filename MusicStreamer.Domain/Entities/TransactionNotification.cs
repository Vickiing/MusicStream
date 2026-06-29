using MusicStreamer.Domain.Enums;
using MusicStreamer.Domain.Services;

namespace MusicStreamer.Domain.Entities;

public sealed class NotificacaoTransacao
{
    public Guid Id { get; private set; }
    public Guid TransactionId { get; private set; }
    public string Recipient { get; private set; } = string.Empty;
    public string Channel { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public StatusNotificacao Status { get; private set; }
    public DateTimeOffset SentAtUtc { get; private set; }

    private NotificacaoTransacao()
    {
    }

    public static NotificacaoTransacao CreateMerchantNotification(Guid transactionId, string merchantName, DecisaoAutorizacaoTransacao decision)
    {
        return Create(transactionId, merchantName, "MerchantWebhook", $"Transacao {decision.Reason}", StatusNotificacao.Delivered);
    }

    public static NotificacaoTransacao CreateCardOwnerNotification(Guid transactionId, string email, DecisaoAutorizacaoTransacao decision)
    {
        return Create(transactionId, email, "Email", $"Sua transacao foi {decision.Reason}", StatusNotificacao.Delivered);
    }

    private static NotificacaoTransacao Create(Guid transactionId, string recipient, string channel, string message, StatusNotificacao status)
    {
        return new NotificacaoTransacao
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


namespace MusicStreamer.App.DTOs;

public sealed record AuthorizeTransactionDto(Guid UserId, Guid MerchantId, decimal Amount, DateTimeOffset RequestedAtUtc);

public sealed record TransactionNotificationDto(string Recipient, string Channel, string Status, string Message);

public sealed record TransactionResultDto(
    Guid TransactionId,
    Guid UserId,
    Guid MerchantId,
    decimal Amount,
    string Status,
    string Reason,
    DateTimeOffset RequestedAtUtc,
    IReadOnlyList<TransactionNotificationDto> Notifications);

namespace MusicStreamer.App.DTOs;

public sealed record AutorizarTransacaoDto(Guid UserId, Guid MerchantId, decimal Amount, DateTimeOffset RequestedAtUtc);

public sealed record NotificacaoTransacaoDto(string Recipient, string Channel, string Status, string Message);

public sealed record ResultadoTransacaoDto(
    Guid TransactionId,
    Guid UserId,
    Guid MerchantId,
    decimal Amount,
    string Status,
    string Reason,
    DateTimeOffset RequestedAtUtc,
    IReadOnlyList<NotificacaoTransacaoDto> Notifications);


namespace MusicStreamer.Domain.Services;

public sealed record TransactionAuthorizationDecision(bool IsApproved, string Reason);

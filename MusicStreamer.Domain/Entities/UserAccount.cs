using MusicStreamer.Domain.Enums;
using MusicStreamer.Domain.ValueObjects;

namespace MusicStreamer.Domain.Entities;

public sealed class UserAccount
{
    public Guid Id { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;
    public EmailAddress Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public AccountStatus Status { get; private set; }
    public Guid? SubscriptionPlanId { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public DateTimeOffset? LastLoginAtUtc { get; private set; }

    private UserAccount()
    {
    }

    public static UserAccount Register(string displayName, string email, string passwordHash)
    {
        return new UserAccount
        {
            Id = Guid.NewGuid(),
            DisplayName = displayName.Trim(),
            Email = new EmailAddress(email),
            PasswordHash = passwordHash,
            Status = AccountStatus.Active,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }

    public void AssignSubscription(Guid planId)
    {
        SubscriptionPlanId = planId;
    }

    public void RegisterLogin(DateTimeOffset instant)
    {
        LastLoginAtUtc = instant;
    }
}

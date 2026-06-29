using MusicStreamer.Domain.Enums;
using MusicStreamer.Domain.ValueObjects;

namespace MusicStreamer.Domain.Entities;

public sealed class ContaUsuario
{
    public Guid Id { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;
    public EnderecoEmail Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public StatusConta Status { get; private set; }
    public Guid? SubscriptionPlanId { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public DateTimeOffset? LastLoginAtUtc { get; private set; }

    private ContaUsuario()
    {
    }

    public static ContaUsuario Register(string displayName, string email, string passwordHash)
    {
        return new ContaUsuario
        {
            Id = Guid.NewGuid(),
            DisplayName = displayName.Trim(),
            Email = new EnderecoEmail(email),
            PasswordHash = passwordHash,
            Status = StatusConta.Active,
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


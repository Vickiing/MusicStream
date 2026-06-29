namespace MusicStreamer.Domain.Entities;

public sealed class PlanoAssinatura
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal MonthlyPrice { get; private set; }
    public decimal MaxTransactionAmount { get; private set; }
    public bool AllowsNightTransactions { get; private set; }
    public bool IsActive { get; private set; }

    private PlanoAssinatura()
    {
    }

    public PlanoAssinatura(string name, decimal monthlyPrice, decimal maxTransactionAmount, bool allowsNightTransactions, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        MonthlyPrice = monthlyPrice;
        MaxTransactionAmount = maxTransactionAmount;
        AllowsNightTransactions = allowsNightTransactions;
        IsActive = isActive;
    }
}


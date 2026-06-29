namespace MusicStreamer.Domain.Entities;

public sealed class Merchant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private Merchant()
    {
    }

    public Merchant(string name, string category, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        Category = category.Trim();
        IsActive = isActive;
    }
}

namespace MusicStreamer.Domain.Entities;

public sealed class Comerciante
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private Comerciante()
    {
    }

    public Comerciante(string name, string category, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        Category = category.Trim();
        IsActive = isActive;
    }
}


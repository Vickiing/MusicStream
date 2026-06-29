namespace MusicStreamer.Domain.ValueObjects;

public sealed class EmailAddress
{
    public string Value { get; }
    public string NormalizedValue { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
        {
            throw new ArgumentException("Email invalido.", nameof(value));
        }

        Value = value.Trim();
        NormalizedValue = Value.ToUpperInvariant();
    }

    public override string ToString() => Value;
}

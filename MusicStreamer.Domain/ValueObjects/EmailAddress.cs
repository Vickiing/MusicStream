namespace MusicStreamer.Domain.ValueObjects;

public sealed class EnderecoEmail
{
    public string Value { get; }
    public string NormalizedValue { get; }

    public EnderecoEmail(string value)
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


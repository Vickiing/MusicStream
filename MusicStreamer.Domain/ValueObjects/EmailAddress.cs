namespace MusicStreamer.Domain.ValueObjects;

public sealed class EnderecoEmail : IEquatable<EnderecoEmail>
{
    public string Value { get; private set; } = string.Empty;
    public string NormalizedValue { get; private set; } = string.Empty;

    private EnderecoEmail()
    {
    }

    public EnderecoEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
        {
            throw new ArgumentException("Email invalido.", nameof(value));
        }

        Value = value.Trim();
        NormalizedValue = Value.ToUpperInvariant();
    }

    public bool Equals(EnderecoEmail? other)
    {
        if (other is null)
        {
            return false;
        }

        return string.Equals(NormalizedValue, other.NormalizedValue, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj) => obj is EnderecoEmail other && Equals(other);

    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(NormalizedValue);

    public static bool operator ==(EnderecoEmail? left, EnderecoEmail? right) => Equals(left, right);

    public static bool operator !=(EnderecoEmail? left, EnderecoEmail? right) => !Equals(left, right);

    public override string ToString() => Value;
}

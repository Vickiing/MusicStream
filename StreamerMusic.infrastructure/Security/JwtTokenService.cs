using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using MusicStreamer.App.Abstractions;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.infrastructure.Security;

public sealed class JwtTokenService(IConfiguration configuration) : ITokenService
{
    public string Generate(UserAccount account)
    {
        var key = configuration["Jwt:Key"] ?? "MusicStreamer-Development-Key-Change-Me-12345";
        var expiresAt = DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds();
        var payload = $"{account.Id}|{account.Email.Value}|{account.DisplayName}|{expiresAt}";
        var signature = Sign(payload, key);
        return $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(payload))}.{signature}";
    }

    private static string Sign(string payload, string key)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        return Convert.ToBase64String(signatureBytes);
    }
}

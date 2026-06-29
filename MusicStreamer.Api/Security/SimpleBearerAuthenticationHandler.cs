using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace MusicStreamer.Api.Security;

public sealed class SimpleBearerAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IConfiguration configuration) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.Authorization.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var token = Request.Headers.Authorization.ToString()["Bearer ".Length..].Trim();
        var parts = token.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            return Task.FromResult(AuthenticateResult.Fail("Token invalido."));
        }

        string payload;
        try
        {
            payload = Encoding.UTF8.GetString(Convert.FromBase64String(parts[0]));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Token malformado."));
        }

        var key = configuration["Jwt:Key"] ?? "MusicStreamer-Development-Key-Change-Me-12345";
        var expectedSignature = Sign(payload, key);
        if (!CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(expectedSignature),
                Convert.FromBase64String(parts[1])))
        {
            return Task.FromResult(AuthenticateResult.Fail("Assinatura invalida."));
        }

        var values = payload.Split('|');
        if (values.Length != 4 || !long.TryParse(values[3], out var expiresAt))
        {
            return Task.FromResult(AuthenticateResult.Fail("Payload invalido."));
        }

        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > expiresAt)
        {
            return Task.FromResult(AuthenticateResult.Fail("Token expirado."));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, values[0]),
            new Claim(ClaimTypes.Email, values[1]),
            new Claim(ClaimTypes.Name, values[2])
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private static string Sign(string payload, string key)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        return Convert.ToBase64String(signatureBytes);
    }
}

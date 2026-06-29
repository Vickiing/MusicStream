using System.ComponentModel.DataAnnotations;

namespace MusicStreamer.Api.Contracts;

public sealed record RegisterRequest(
    [property: Required, StringLength(120)] string DisplayName,
    [property: Required, EmailAddress] string Email,
    [property: Required, MinLength(6)] string Password);

public sealed record LoginRequest(
    [property: Required, EmailAddress] string Email,
    [property: Required] string Password);


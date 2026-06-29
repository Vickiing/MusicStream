namespace MusicStreamer.App.DTOs;

public sealed record RegisterUserDto(string DisplayName, string Email, string Password);

public sealed record LoginDto(string Email, string Password);

public sealed record AuthResponseDto(Guid UserId, string DisplayName, string Email, string Token);

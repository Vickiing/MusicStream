using MusicStreamer.App.DTOs;

namespace MusicStreamer.App.Contracts;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterUserDto input, CancellationToken cancellationToken = default);
    Task<AuthResponseDto?> LoginAsync(LoginDto input, CancellationToken cancellationToken = default);
}

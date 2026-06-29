using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IServicoAutenticacao authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RespostaAutenticacaoDto>> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.RegisterAsync(
            new CadastrarUsuarioDto(request.DisplayName, request.Email, request.Password),
            cancellationToken);

        return CreatedAtAction(nameof(Register), new { response.UserId }, response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<RespostaAutenticacaoDto>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(new EntrarDto(request.Email, request.Password), cancellationToken);
        return response is null ? Unauthorized() : Ok(response);
    }
}


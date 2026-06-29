using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Contracts;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.DTOs;

namespace MusicStreamer.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AutenticacaoController(IServicoAutenticacao servicoAutenticacao) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RespostaAutenticacaoDto>> Register([FromBody] CadastroRequest request, CancellationToken cancellationToken)
    {
        var response = await servicoAutenticacao.RegisterAsync(
            new CadastrarUsuarioDto(request.DisplayName, request.Email, request.Password),
            cancellationToken);

        return CreatedAtAction(nameof(Register), new { response.UserId }, response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<RespostaAutenticacaoDto>> Login([FromBody] EntrarRequest request, CancellationToken cancellationToken)
    {
        var response = await servicoAutenticacao.LoginAsync(new EntrarDto(request.Email, request.Password), cancellationToken);
        return response is null ? Unauthorized() : Ok(response);
    }
}


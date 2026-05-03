using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Dtos;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.Inputs;

namespace MusicStreamer.Api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class CadastrarUsuarioController(IUsuarioApp usuarioApp) : ControllerBase
    {
        private readonly IUsuarioApp _usuarioApp = usuarioApp;

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioRequest usuario)
        {
            if (usuario is null)
            {
                return BadRequest("Envie o corpo como JSON no formato esperado pela API.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var input = new CadastrarUsuarioInput
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Cpf = usuario.Cpf
            };

            var resultado = await _usuarioApp.CadastrarUsuarioAsync(input);

            if (!resultado)
            {
                return Conflict("Usuario ja cadastrado ou dados invalidos.");
            }

            return Created(string.Empty, new { mensagem = "Usuario cadastrado com sucesso!" });
        }
    }
}

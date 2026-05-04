using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Dtos;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.Inputs;

namespace MusicStreamer.Api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController(IUsuarioApplicationService usuarioApplicationService) : ControllerBase
    {
        private readonly IUsuarioApplicationService _usuarioApplicationService = usuarioApplicationService;

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioRequest usuarioRequest)
        {
            if (usuarioRequest is null)
            {
                return BadRequest("Envie o corpo como JSON no formato esperado pela API.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var input = new CadastrarUsuarioInput
            {
                Nome = usuarioRequest.Nome,
                Email = usuarioRequest.Email,
                Senha = usuarioRequest.Senha,
                Cpf = usuarioRequest.Cpf
            };

            var resultado = await _usuarioApplicationService.CadastrarUsuarioAsync(input);

            if (!resultado)
            {
                return Conflict("Usuario ja cadastrado ou dados invalidos.");
            }

            return Created(string.Empty, new { mensagem = "Usuario cadastrado com sucesso!" });
        }
    }
}

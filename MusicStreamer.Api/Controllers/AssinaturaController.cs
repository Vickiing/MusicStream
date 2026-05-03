using Microsoft.AspNetCore.Mvc;
using MusicStreamer.App.Contracts;

namespace MusicStreamer.Api.Controllers
{
    [Route("api/[controller]")]
    public class AssinaturaController(IAssinaturaApp assinaturaApp) : ControllerBase
    {
        private readonly IAssinaturaApp _assinaturaApp = assinaturaApp;
        [HttpPost("ativar")]
        public async Task<IActionResult> AtivarAssinatura(int usuarioId)
        {
            var resultado = await _assinaturaApp.AtivarAssinaturaAsync(usuarioId);
            if (!resultado)
            {
                return BadRequest("Não foi possível ativar a assinatura. Verifique o ID do usuário e tente novamente.");
            }
            return Ok(new { mensagem = "Assinatura ativada com sucesso!" });
        }

        [HttpPost("cancelar")]
        public async Task<IActionResult> CancelarAssinatura(int usuarioId)
        {
            var resultado = await _assinaturaApp.CancelarAssinaturaAsync(usuarioId);
            
            if (!resultado)
            {
                return BadRequest("Não foi possível cancelar a assinatura. Verifique o ID do usuário e tente novamente.");
            }
            return Ok(new { mensagem = "Assinatura cancelada com sucesso!" });
        }
    }
}

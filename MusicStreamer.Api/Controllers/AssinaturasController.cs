using Microsoft.AspNetCore.Mvc;
using MusicStreamer.Api.Dtos;
using MusicStreamer.App.Contracts;
using MusicStreamer.App.Inputs;

namespace MusicStreamer.Api.Controllers
{
    [ApiController]
    [Route("api/assinaturas")]
    public class AssinaturasController(IAssinaturaApplicationService assinaturaApplicationService) : ControllerBase
    {
        private readonly IAssinaturaApplicationService _assinaturaApplicationService = assinaturaApplicationService;

        [HttpPost("ativar")]
        public async Task<IActionResult> AtivarAssinatura([FromBody] AtivarAssinaturaRequest ativarAssinaturaRequest)
        {
            if (ativarAssinaturaRequest is null)
            {
                return BadRequest("Dados de pagamento inválidos. Verifique as informações fornecidas e tente novamente.");
            }

            var input = new AtivarAssinaturaInput
            {
                UsuarioId = ativarAssinaturaRequest.UsuarioId,
                TipoPagamento = ativarAssinaturaRequest.TipoPagamento,
                TipoAssinatura = ativarAssinaturaRequest.TipoAssinatura,
                RenovacaoAutomatica = ativarAssinaturaRequest.RenovacaoAutomatica
            };

            var resultado = await _assinaturaApplicationService.AtivarAssinaturaAsync(input);

            if (!resultado)
            {
                return BadRequest("Não foi possível ativar a assinatura. Verifique os dados fornecidos e tente novamente.");
            }

            return Ok(new { mensagem = "Assinatura ativada com sucesso!" });
        }

        [HttpPost("cancelar")]
        public async Task<IActionResult> CancelarAssinatura(Guid usuarioId)
        {
            var resultado = await _assinaturaApplicationService.CancelarAssinaturaAsync(usuarioId);

            if (!resultado)
            {
                return BadRequest("Não foi possível cancelar a assinatura. Verifique o ID do usuário e tente novamente.");
            }

            return Ok(new { mensagem = "Assinatura cancelada com sucesso!" });
        }
    }
}

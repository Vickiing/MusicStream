namespace MusicStreamer.Api.Dtos
{
    public class AtivarAssinaturaRequest
    {
        public int UsuarioId { get; set; }
        public int TipoPagamento { get; set; }
        public int TipoAssinatura { get; set; }
        public bool RenovacaoAutomatica { get; set; }
    }
}

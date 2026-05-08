using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStreamer.Domain.Entity
{
    
    public class AssinaturaEntity
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public int TipoAssinatura { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool RenovacaoAutomatica { get; set; }
        public DateTime DataFimAutomatica { get; set; }
    }
}

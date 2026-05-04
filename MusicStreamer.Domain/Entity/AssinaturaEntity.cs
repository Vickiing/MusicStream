using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStreamer.Domain.Entity
{
    [Table("Assinatura")]
    public class AssinaturaEntity
    {
        [Column("IdAssinatura")]
        public int Id { get; set; }
        [Column("IdUsuario")]
        public int UsuarioId { get; set; }
        [Column("TipoAssinatura")]
        public int TipoAssinatura { get; set; }
        [Column("Ativa")]
        public bool Ativa { get; set; }
        [Column("DataInicio")]
        public DateTime DataInicio { get; set; }
        [Column("DataFim")]
        public DateTime DataFim { get; set; }
        [Column("RenovacaoAutomatica")]
        public bool RenovacaoAutomatica { get; set; }
        [Column("DataFimAutomatica")]
        public DateTime DataFimAutomatica { get; set; }
    }
}

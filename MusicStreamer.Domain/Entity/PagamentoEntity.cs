using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStreamer.Domain.Entity
{
    [Table("Pagamento")]
    public class PagamentoEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("tipoPagamento")]
        public int TipoPagamento { get; set; }
        [Column("valor")]
        public decimal Valor { get; set; }
    }
}

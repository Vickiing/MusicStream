using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStreamer.Domain.Entity
{
    public class PagamentoEntity
    {
        public int Id { get; set; }
        public int TipoPagamento { get; set; }
        public decimal Valor { get; set; }
    }
}

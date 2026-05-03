namespace MusicStreamer.Domain.Entity
{
    public class AssinaturaEntity
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool RenovacaoAutomatica { get; set; }
        public DateTime DataFimAutomatica { get; set; }
    }
}

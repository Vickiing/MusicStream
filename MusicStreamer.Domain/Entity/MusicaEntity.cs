using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStreamer.Domain.Entity
{
    [Table("Musica")]
    public class MusicaEntity
    {
        [Column("IdMusica")]
        public int Id { get; set; }
        [Column("Nome")]
        public required string Nome { get; set; }
        [Column("Artista")]
        public required string Artista { get; set; }
        [Column("Titulo")]
        public required string Titulo { get; set; }
        [Column("Album")]
        public string? Album { get; set; }
    }
}
